using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using Windows.Storage.Streams;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Maui.Handlers
{
	public partial class HybridWebViewHandler : ViewHandler<IHybridWebView, WebView2>
	{
		// Using an IP address means that WebView2 doesn't wait for any DNS resolution,
		// making it substantially faster. Note that this isn't real HTTP traffic, since
		// we intercept all the requests within this origin.
		private static readonly string AppHostAddress = "0.0.0.0";

		/// <summary>
		/// Gets the application's base URI. Defaults to <c>https://0.0.0.0/</c>
		/// </summary>
		private static readonly string AppOrigin = $"https://{AppHostAddress}/";

		private static readonly Uri AppOriginUri = new(AppOrigin);

		//WebNavigationEvent _eventState;
		//readonly WebView2Proxy _proxy = new();
		//readonly HashSet<string> _loadedCookies = new();
		//Window? _window;

		protected override WebView2 CreatePlatformView()
		{
			var logger = this.MauiContext!.Services!.GetService<ILogger<HybridWebViewHandler>>() ?? NullLogger<HybridWebViewHandler>.Instance;
			logger.LogInformation("HybridWebViewHandler: CreatePlatformView WebView2");

			return new HybridPlatformWebView(this);
		}

		protected override async void ConnectHandler(WebView2 platformView)
		{
			var logger = this.MauiContext!.Services!.GetService<ILogger<HybridWebViewHandler>>() ?? NullLogger<HybridWebViewHandler>.Instance;
			logger.LogInformation("HybridWebViewHandler: Connecting WebView2");

			base.ConnectHandler(platformView);


			platformView.WebMessageReceived += OnWebMessageReceived;

			await platformView.EnsureCoreWebView2Async();

			platformView.CoreWebView2.Settings.AreDevToolsEnabled = true;//EnableWebDevTools;
			platformView.CoreWebView2.Settings.IsWebMessageEnabled = true;
			platformView.CoreWebView2.AddWebResourceRequestedFilter($"{AppOrigin}*", CoreWebView2WebResourceContext.All);
			platformView.CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;


			platformView.Source = new Uri(new Uri(AppOriginUri, "/").ToString());

		}

		protected override void DisconnectHandler(WebView2 platformView)
		{
			var logger = this.MauiContext!.Services!.GetService<ILogger<HybridWebViewHandler>>() ?? NullLogger<HybridWebViewHandler>.Instance;
			logger.LogInformation("HybridWebViewHandler: Disconnecting WebView2");

			base.DisconnectHandler(platformView);
		}
		//protected override void ConnectHandler(WebView2 platformView)
		//{
		//	_proxy.Connect(this, platformView);
		//	base.ConnectHandler(platformView);

		//	if (platformView.IsLoaded)
		//		OnLoaded();
		//	else
		//		platformView.Loaded += OnWebViewLoaded;
		//}

		//void OnWebViewLoaded(object sender, RoutedEventArgs e)
		//{
		//	OnLoaded();
		//}

		//void OnLoaded()
		//{
		//	_window = MauiContext!.GetPlatformWindow();
		//	_window.Closed += OnWindowClosed;
		//}

		//private void OnWindowClosed(object sender, WindowEventArgs args)
		//{
		//	Disconnect(PlatformView);
		//}

		//void Disconnect(WebView2 platformView)
		//{
		//	if (_window is not null)
		//	{
		//		_window.Closed -= OnWindowClosed;
		//		_window = null;
		//	}

		//	platformView.Loaded -= OnWebViewLoaded;
		//	_proxy.Disconnect(platformView);
		//	platformView.Close();
		//}

		//protected override void DisconnectHandler(WebView2 platformView)
		//{
		//	Disconnect(platformView);
		//	base.DisconnectHandler(platformView);
		//}


		public static void MapSendRawMessage(IHybridWebViewHandler handler, IHybridWebView hybridWebView, object? arg)
		{
			if (arg is not string rawMessage || handler.PlatformView is not IHybridPlatformWebView hybridPlatformWebView)
			{
				return;
			}

			hybridPlatformWebView.SendRawMessage(rawMessage);
		}


		private void OnWebMessageReceived(WebView2 sender, CoreWebView2WebMessageReceivedEventArgs args)
		{
			VirtualView?.RawMessageReceived(args.TryGetWebMessageAsString());
		}

		private async void CoreWebView2_WebResourceRequested(CoreWebView2 sender, CoreWebView2WebResourceRequestedEventArgs eventArgs)
		{
			// Get a deferral object so that WebView2 knows there's some async stuff going on. We call Complete() at the end of this method.
			using var deferral = eventArgs.GetDeferral();

			var requestUri = HybridWebViewQueryStringHelper.RemovePossibleQueryString(eventArgs.Request.Uri);

			if (new Uri(requestUri) is Uri uri && AppOriginUri.IsBaseOf(uri))
			{
				var relativePath = AppOriginUri.MakeRelativeUri(uri).ToString().Replace('/', '\\');

				string contentType;
				if (string.IsNullOrEmpty(relativePath))
				{
					relativePath = VirtualView.DefaultFile;
					contentType = "text/html";
				}
				else
				{
					var requestExtension = Path.GetExtension(relativePath);
					contentType = requestExtension switch
					{
						".htm" or ".html" => "text/html",
						".js" => "application/javascript",
						".css" => "text/css",
						_ => "text/plain",
					};
				}

				Stream? contentStream = null;

				//contentStream = KnownStaticFileProvider.GetKnownResourceStream(relativePath!);

				//if (contentStream is null)
				//{
				var assetPath = Path.Combine(VirtualView.HybridRoot!, relativePath!);
				contentStream = await GetAssetStreamAsync(assetPath);
				//}

				if (contentStream is null)
				{
					var notFoundContent = "Resource not found (404)";
					eventArgs.Response = sender.Environment!.CreateWebResourceResponse(
						Content: null,
						StatusCode: 404,
						ReasonPhrase: "Not Found",
						Headers: GetHeaderString("text/plain", notFoundContent.Length)
					);
				}
				else
				{
					eventArgs.Response = sender.Environment!.CreateWebResourceResponse(
						Content: await CopyContentToRandomAccessStreamAsync(contentStream),
						StatusCode: 200,
						ReasonPhrase: "OK",
						Headers: GetHeaderString(contentType, (int)contentStream.Length)
					);
				}

				contentStream?.Dispose();
			}

			// Notify WebView2 that the deferred (async) operation is complete and we set a response.
			deferral.Complete();

			async Task<IRandomAccessStream> CopyContentToRandomAccessStreamAsync(Stream content)
			{
				using var memStream = new MemoryStream();
				await content.CopyToAsync(memStream);
				var randomAccessStream = new InMemoryRandomAccessStream();
				await randomAccessStream.WriteAsync(memStream.GetWindowsRuntimeBuffer());
				return randomAccessStream;
			}
		}

		private protected static string GetHeaderString(string contentType, int contentLength) =>
$@"Content-Type: {contentType}
Content-Length: {contentLength}";

		protected class HybridPlatformWebView : WebView2, IHybridPlatformWebView
		{
			private readonly WeakReference<HybridWebViewHandler> _handler;

			public HybridPlatformWebView(HybridWebViewHandler handler)
			{
				ArgumentNullException.ThrowIfNull(handler, nameof(handler));
				_handler = new WeakReference<HybridWebViewHandler>(handler);
			}

			public void SendRawMessage(string rawMessage)
			{
				CoreWebView2.PostWebMessageAsString(rawMessage);
			}
		}
	}
}