#nullable disable
using System;

namespace Microsoft.Maui.Controls
{
	public class WebViewProcessTerminatedEventArgs : EventArgs
	{
		public WebViewProcessTerminatedEventArgs(WebProcessTerminated webProcessTerminated)
		{
#if ANDROID
			RenderProcessGoneDetail = webProcessTerminated.RenderProcessGoneDetail;
#elif WINDOWS
			CoreWebView2ProcessFailedEventArgs = webProcessTerminated.CoreWebView2ProcessFailedEventArgs;
#endif
		}

#if ANDROID
		public Android.Webkit.RenderProcessGoneDetail RenderProcessGoneDetail { get; }
#elif WINDOWS
		public Web.WebView2.Core.CoreWebView2ProcessFailedEventArgs CoreWebView2ProcessFailedEventArgs { get; }
#endif
	}
}