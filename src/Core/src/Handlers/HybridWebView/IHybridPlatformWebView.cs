namespace Microsoft.Maui.Handlers
{
	public interface IHybridPlatformWebView
	{
		void SendRawMessage(string rawMessage);
		void SetHybridRoot(string? hybridRoot);

	}
}
