using UITest.Core;

namespace UITest.Appium;

public class AppiumCatalystAlertActions : AppiumAppleAlertActions
{
	public AppiumCatalystAlertActions(AppiumApp appiumApp)
		: base(appiumApp)
	{
	}

	protected override IReadOnlyCollection<IUIElement> OnGetAlerts(AppiumApp appiumApp, IDictionary<string, object> parameters)
	{
		// Catalyst always uses action sheets.
		var alerts = appiumApp.FindElements(AppiumQuery.ByClass("XCUIElementTypeSheet"));

		return alerts;
	}
}
