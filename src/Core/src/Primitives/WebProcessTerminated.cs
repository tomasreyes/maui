namespace Microsoft.Maui
{
	public class WebProcessTerminated
	{
#if ANDROID
		public WebProcessTerminated(Android.Webkit.RenderProcessGoneDetail? renderProcessGoneDetail)
		{
			RenderProcessGoneDetail = renderProcessGoneDetail;
		}

		public Android.Webkit.RenderProcessGoneDetail? RenderProcessGoneDetail { get; }
#elif WINDOWS
		public WebProcessTerminated(Web.WebView2.Core.CoreWebView2ProcessFailedEventArgs coreWebView2ProcessFailedEventArgs)
		{
			CoreWebView2ProcessFailedEventArgs = coreWebView2ProcessFailedEventArgs;
		}

		public Web.WebView2.Core.CoreWebView2ProcessFailedEventArgs CoreWebView2ProcessFailedEventArgs { get; }
#endif
	}
}
