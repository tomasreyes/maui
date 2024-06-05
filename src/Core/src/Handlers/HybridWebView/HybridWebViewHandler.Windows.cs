using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using Windows.Storage.Streams;

namespace Microsoft.Maui.Handlers
{
	public partial class HybridWebViewHandler : ViewHandler<IHybridWebView, WebView2>
	{
		//WebNavigationEvent _eventState;
		//readonly WebView2Proxy _proxy = new();
		//readonly HashSet<string> _loadedCookies = new();
		//Window? _window;

		protected override WebView2 CreatePlatformView() => new HybridPlatformWebView(this);

		//internal WebNavigationEvent CurrentNavigationEvent
		//{
		//	get => _eventState;
		//	set => _eventState = value;
		//}

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

		//void OnNavigationStarting(CoreWebView2 sender, CoreWebView2NavigationStartingEventArgs args)
		//{
		//	if (Uri.TryCreate(args.Uri, UriKind.Absolute, out Uri? uri) && uri is not null)
		//	{
		//		bool cancel = VirtualView.Navigating(CurrentNavigationEvent, uri.AbsoluteUri);

		//		args.Cancel = cancel;

		//		// Reset in this case because this is the last event we will get
		//		if (cancel)
		//			_eventState = WebNavigationEvent.NewPage;
		//	}
		//}

		public static void MapHybridRoot(IHybridWebViewHandler handler, IHybridWebView hybridWebView)
		{
			if (handler.PlatformView is IHybridPlatformWebView hybridPlatformWebView)
			{
				hybridPlatformWebView.SetHybridRoot(hybridWebView.HybridRoot);
			}
		}


		//private void Wv2_WebMessageReceived(Microsoft.UI.Xaml.Controls.WebView2 sender, CoreWebView2WebMessageReceivedEventArgs args)
		//{
		//	OnMessageReceived(args.TryGetWebMessageAsString());
		//}

		//public static void MapUserAgent(IWebViewHandler handler, IWebView webView)
		//{
		//	handler.PlatformView?.UpdateUserAgent(webView);
		//}

		//public static void MapGoBack(IWebViewHandler handler, IWebView webView, object? arg)
		//{
		//	if (handler.PlatformView.CanGoBack && handler is WebViewHandler w)
		//		w.CurrentNavigationEvent = WebNavigationEvent.Back;

		//	handler.PlatformView?.UpdateGoBack(webView);
		//}

		//public static void MapGoForward(IWebViewHandler handler, IWebView webView, object? arg)
		//{
		//	if (handler.PlatformView.CanGoForward && handler is WebViewHandler w)
		//		w.CurrentNavigationEvent = WebNavigationEvent.Forward;

		//	handler.PlatformView?.UpdateGoForward(webView);
		//}

		//public static void MapReload(IWebViewHandler handler, IWebView webView, object? arg)
		//{
		//	if (handler is WebViewHandler w)
		//		w.CurrentNavigationEvent = WebNavigationEvent.Refresh;

		//	handler.PlatformView?.UpdateReload(webView);
		//}

		//public static void MapEval(IWebViewHandler handler, IWebView webView, object? arg)
		//{
		//	if (arg is not string script)
		//		return;

		//	handler.PlatformView?.Eval(webView, script);
		//}

		//void NavigationSucceeded(CoreWebView2 sender, CoreWebView2NavigationCompletedEventArgs e)
		//{
		//	var uri = sender.Source;

		//	if (uri is not null)
		//		SendNavigated(uri, CurrentNavigationEvent, WebNavigationResult.Success);

		//	if (VirtualView is null)
		//		return;

		//	PlatformView?.UpdateCanGoBackForward(VirtualView);
		//}

		//void NavigationFailed(CoreWebView2 sender, CoreWebView2NavigationCompletedEventArgs e)
		//{
		//	var uri = sender.Source;

		//	if (!string.IsNullOrEmpty(uri))
		//		SendNavigated(uri, CurrentNavigationEvent, WebNavigationResult.Failure);
		//}

		//async void SendNavigated(string url, WebNavigationEvent evnt, WebNavigationResult result)
		//{
		//	if (VirtualView is not null)
		//	{
		//		await SyncPlatformCookiesToVirtualView(url);

		//		VirtualView.Navigated(evnt, url, result);
		//		PlatformView?.UpdateCanGoBackForward(VirtualView);
		//	}

		//	CurrentNavigationEvent = WebNavigationEvent.NewPage;
		//}

		//async Task SyncPlatformCookiesToVirtualView(string url)
		//{
		//	var myCookieJar = VirtualView.Cookies;

		//	if (myCookieJar is null)
		//		return;

		//	var uri = CreateUriForCookies(url);

		//	if (uri is null)
		//		return;

		//	var cookies = myCookieJar.GetCookies(uri);
		//	var retrieveCurrentWebCookies = await GetCookiesFromPlatformStore(url);

		//	var platformCookies = await PlatformView.CoreWebView2.CookieManager.GetCookiesAsync(uri.AbsoluteUri);

		//	foreach (Cookie cookie in cookies)
		//	{
		//		var httpCookie = platformCookies
		//			.FirstOrDefault(x => x.Name == cookie.Name);

		//		if (httpCookie is null)
		//			cookie.Expired = true;
		//		else
		//			cookie.Value = httpCookie.Value;
		//	}

		//	await SyncPlatformCookies(url);
		//}

		//internal async Task SyncPlatformCookies(string url)
		//{
		//	var uri = CreateUriForCookies(url);

		//	if (uri is null)
		//		return;

		//	var myCookieJar = VirtualView.Cookies;

		//	if (myCookieJar is null)
		//		return;

		//	await InitialCookiePreloadIfNecessary(url);
		//	var cookies = myCookieJar.GetCookies(uri);

		//	if (cookies is null)
		//		return;

		//	var retrieveCurrentWebCookies = await GetCookiesFromPlatformStore(url);

		//	foreach (Cookie cookie in cookies)
		//	{
		//		var createdCookie = PlatformView.CoreWebView2.CookieManager.CreateCookie(cookie.Name, cookie.Value, cookie.Domain, cookie.Path);
		//		PlatformView.CoreWebView2.CookieManager.AddOrUpdateCookie(createdCookie);
		//	}

		//	foreach (CoreWebView2Cookie cookie in retrieveCurrentWebCookies)
		//	{
		//		if (cookies[cookie.Name] is not null)
		//			continue;

		//		PlatformView.CoreWebView2.CookieManager.DeleteCookie(cookie);
		//	}
		//}

		//async Task InitialCookiePreloadIfNecessary(string url)
		//{
		//	var myCookieJar = VirtualView.Cookies;

		//	if (myCookieJar is null)
		//		return;

		//	var uri = new Uri(url);

		//	if (!_loadedCookies.Add(uri.Host))
		//		return;

		//	var cookies = myCookieJar.GetCookies(uri);

		//	if (cookies is not null)
		//	{
		//		var existingCookies = await GetCookiesFromPlatformStore(url);

		//		if (existingCookies.Count == 0)
		//			return;

		//		foreach (CoreWebView2Cookie cookie in existingCookies)
		//		{
		//			// TODO Ideally we use cookie.ToSystemNetCookie() here, but it's not available for some reason check back later
		//			if (cookies[cookie.Name] is null)
		//				myCookieJar.SetCookies(uri,
		//					new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain)
		//					{
		//						Expires = DateTimeOffset.FromUnixTimeMilliseconds((long)cookie.Expires).DateTime,
		//						HttpOnly = cookie.IsHttpOnly,
		//						Secure = cookie.IsSecure,
		//					}.ToString());
		//		}
		//	}
		//}

		//Task<IReadOnlyList<CoreWebView2Cookie>> GetCookiesFromPlatformStore(string url)
		//{
		//	return PlatformView.CoreWebView2.CookieManager.GetCookiesAsync(url).AsTask();
		//}

		//static Uri? CreateUriForCookies(string url)
		//{
		//	if (url is null)
		//		return null;

		//	Uri? uri;

		//	if (url.Length > 2000)
		//		url = url.Substring(0, 2000);

		//	if (Uri.TryCreate(url, UriKind.Absolute, out uri))
		//	{
		//		if (string.IsNullOrWhiteSpace(uri.Host))
		//			return null;

		//		return uri;
		//	}

		//	return null;
		//}

		//public static void MapEvaluateJavaScriptAsync(IWebViewHandler handler, IWebView webView, object? arg)
		//{
		//	if (arg is EvaluateJavaScriptAsyncRequest request)
		//	{
		//		if (handler.PlatformView is null)
		//		{
		//			request.SetCanceled();
		//			return;
		//		}

		//		handler.PlatformView.EvaluateJavaScript(request);
		//	}
		//}

		//class WebView2Proxy
		//{
		//	WeakReference<WebViewHandler>? _handler;

		//	WebViewHandler? Handler => _handler is not null && _handler.TryGetTarget(out var h) ? h : null;

		//	public void Connect(WebViewHandler handler, WebView2 platformView)
		//	{
		//		_handler = new(handler);
		//		platformView.CoreWebView2Initialized += OnCoreWebView2Initialized;
		//	}

		//	public void Disconnect(WebView2 platformView)
		//	{
		//		platformView.CoreWebView2Initialized -= OnCoreWebView2Initialized;

		//		if (platformView.CoreWebView2 is CoreWebView2 webView2)
		//		{
		//			webView2.HistoryChanged -= OnHistoryChanged;
		//			webView2.NavigationStarting -= OnNavigationStarting;
		//			webView2.NavigationCompleted -= OnNavigationCompleted;
		//			webView2.Stop();
		//		}

		//		_handler = null;
		//	}

		//	void OnCoreWebView2Initialized(WebView2 sender, CoreWebView2InitializedEventArgs args)
		//	{
		//		sender.CoreWebView2.HistoryChanged += OnHistoryChanged;
		//		sender.CoreWebView2.NavigationStarting += OnNavigationStarting;
		//		sender.CoreWebView2.NavigationCompleted += OnNavigationCompleted;

		//		if (Handler is WebViewHandler handler)
		//		{
		//			sender.UpdateUserAgent(handler.VirtualView);
		//		}
		//	}

		//	void OnHistoryChanged(CoreWebView2 sender, object args)
		//	{
		//		if (Handler is WebViewHandler handler)
		//		{
		//			handler.PlatformView?.UpdateCanGoBackForward(handler.VirtualView);
		//		}
		//	}

		//	void OnNavigationCompleted(CoreWebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
		//	{
		//		if (Handler is WebViewHandler handler)
		//		{
		//			if (args.IsSuccess)
		//				handler.NavigationSucceeded(sender, args);
		//			else
		//				handler.NavigationFailed(sender, args);
		//		}
		//	}

		//	void OnNavigationStarting(CoreWebView2 sender, CoreWebView2NavigationStartingEventArgs args)
		//	{
		//		if (Handler is WebViewHandler handler)
		//		{
		//			handler.OnNavigationStarting(sender, args);
		//		}
		//	}
		//}

		protected class HybridPlatformWebView : WebView2, IHybridPlatformWebView
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


			private readonly WeakReference<HybridWebViewHandler> _handler;
			private string? _hybridRoot;

			public HybridPlatformWebView(HybridWebViewHandler handler)
			{
				ArgumentNullException.ThrowIfNull(handler, nameof(handler));
				_handler = new WeakReference<HybridWebViewHandler>(handler);
			}

			public async void SetHybridRoot(string? hybridRoot)
			{
				_hybridRoot = hybridRoot;
				//wv2.WebMessageReceived += Wv2_WebMessageReceived;

				//_coreWebView2Environment = await CoreWebView2Environment.CreateAsync();

				await EnsureCoreWebView2Async();

				CoreWebView2.Settings.AreDevToolsEnabled = true;//EnableWebDevTools;
				CoreWebView2.Settings.IsWebMessageEnabled = true;
				CoreWebView2.AddWebResourceRequestedFilter($"{AppOrigin}*", CoreWebView2WebResourceContext.All);
				CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;


				Source = new Uri(new Uri(AppOriginUri, "/").ToString());

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
						relativePath = "index.html";//MainFile;
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
					var assetPath = Path.Combine(_hybridRoot!, relativePath!);
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

		}
	}
}