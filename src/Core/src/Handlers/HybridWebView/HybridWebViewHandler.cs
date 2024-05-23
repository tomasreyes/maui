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

namespace Microsoft.Maui.Handlers
{
	public partial class HybridWebViewHandler : IHybridWebViewHandler
	{
        public static IPropertyMapper<IHybridWebView, IHybridWebViewHandler> Mapper = new PropertyMapper<IHybridWebView, IHybridWebViewHandler>(ViewHandler.ViewMapper)
        {
//            [nameof(IWebView.Source)] = MapSource,
//            [nameof(IWebView.UserAgent)] = MapUserAgent,
//#if __ANDROID__
//			[nameof(WebViewClient)] = MapWebViewClient,
//			[nameof(WebChromeClient)] = MapWebChromeClient,
//			[nameof(WebView.Settings)] =  MapWebViewSettings
//#elif __IOS__
//			[nameof(WKUIDelegate)] = MapWKUIDelegate,
//#endif
        };

        public static CommandMapper<IHybridWebView, IHybridWebViewHandler> CommandMapper = new(ViewCommandMapper)
        {
            //[nameof(IWebView.GoBack)] = MapGoBack,
            //[nameof(IWebView.GoForward)] = MapGoForward,
            //[nameof(IWebView.Reload)] = MapReload,
            //[nameof(IWebView.Eval)] = MapEval,
            //[nameof(IWebView.EvaluateJavaScriptAsync)] = MapEvaluateJavaScriptAsync,
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
    }
}
