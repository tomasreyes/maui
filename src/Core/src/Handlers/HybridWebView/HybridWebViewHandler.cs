#if __IOS__ || MACCATALYST
using PlatformView = WebKit.WKWebView;
#elif MONOANDROID
using PlatformView = Android.Webkit.WebView;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.WebView2;
#elif TIZEN
using PlatformView = Microsoft.Maui.Platform.MauiWebView;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformView = System.Object;
#endif
#if __ANDROID__
using Android.Webkit;
#elif __IOS__
using WebKit;
#endif
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace Microsoft.Maui.Handlers
{
	public partial class HybridWebViewHandler : IHybridWebViewHandler
	{
        public static IPropertyMapper<IHybridWebView, IHybridWebViewHandler> Mapper = new PropertyMapper<IHybridWebView, IHybridWebViewHandler>(ViewHandler.ViewMapper)
        {
            [nameof(IHybridWebView.DefaultFile)] = MapDefaultFile,
            [nameof(IHybridWebView.HybridRoot)] = MapHybridRoot,
		};

        public static CommandMapper<IHybridWebView, IHybridWebViewHandler> CommandMapper = new(ViewCommandMapper)
        {
            [nameof(IHybridWebView.SendRawMessage)] = MapSendRawMessage,
        };

        public HybridWebViewHandler() : base(Mapper, CommandMapper)
        {
        }

        public HybridWebViewHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null)
            : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
        {
        }

        IHybridWebView IHybridWebViewHandler.VirtualView => VirtualView;

        PlatformView IHybridWebViewHandler.PlatformView => PlatformView;

		public static void MapDefaultFile(IHybridWebViewHandler handler, IHybridWebView hybridWebView)
		{
			handler.DefaultFile = hybridWebView.DefaultFile;
		}

		public static void MapHybridRoot(IHybridWebViewHandler handler, IHybridWebView hybridWebView)
		{
			handler.HybridRoot = hybridWebView.HybridRoot;
		}

		public string? DefaultFile { get; set; }
		public string? HybridRoot { get; set; }

		internal static async Task<string?> GetAssetContentAsync(string assetPath)
		{
			using var stream = await GetAssetStreamAsync(assetPath);
			if (stream == null)
			{
				return null;
			}
			using var reader = new StreamReader(stream);

			var contents = reader.ReadToEnd();

			return contents;
		}

		internal static async Task<Stream?> GetAssetStreamAsync(string assetPath)
		{
			if (!await FileSystem.AppPackageFileExistsAsync(assetPath))
			{
				return null;
			}
			return await FileSystem.OpenAppPackageFileAsync(assetPath);
		}
	}
}
