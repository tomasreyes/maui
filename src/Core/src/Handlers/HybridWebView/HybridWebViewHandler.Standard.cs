using System;

namespace Microsoft.Maui.Handlers
{
    public partial class HybridWebViewHandler : ViewHandler<IHybridWebView, object>
	{
		protected override object CreatePlatformView() => throw new NotImplementedException();

		//public static void MapSource(IHybridWebViewHandler handler, IHybridWebView webView) { }
		//public static void MapUserAgent(IHybridWebViewHandler handler, IHybridWebView webView) { }

		//public static void MapGoBack(IHybridWebViewHandler handler, IHybridWebView webView, object? arg) { }
		//public static void MapGoForward(IHybridWebViewHandler handler, IHybridWebView webView, object? arg) { }
		//public static void MapReload(IHybridWebViewHandler handler, IHybridWebView webView, object? arg) { }
		//public static void MapEval(IHybridWebViewHandler handler, IHybridWebView webView, object? arg) { }
		//public static void MapEvaluateJavaScriptAsync(IHybridWebViewHandler handler, IHybridWebView webView, object? arg) { }
	}
}