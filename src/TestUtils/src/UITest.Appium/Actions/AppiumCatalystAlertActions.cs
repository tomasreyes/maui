using UITest.Core;

namespace UITest.Appium;

public class AppiumCatalystAlertActions : AppiumAppleAlertActions
{
	// Selects the inner "popover contents" of a popover window.
	const string PossibleActionSheetXPath = 
		"//XCUIElementTypeWindow/XCUIElementTypePopover/XCUIElementTypeWindow/XCUIElementTypePopover";

	public AppiumCatalystAlertActions(AppiumApp appiumApp)
		: base(appiumApp)
	{
	}

	protected override IReadOnlyCollection<IUIElement> OnGetAlerts(AppiumApp appiumApp, IDictionary<string, object> parameters)
	{
		// Catalyst uses action sheets for alerts and macOS 14
		var alerts = appiumApp.FindElements(AppiumQuery.ByClass("XCUIElementTypeSheet"));

		// But it also uses popovers for action sheets on macOS 13
		if (alerts is null || alerts.Count == 0)
			alerts = appiumApp.FindElements(AppiumQuery.ByXPath(PossibleActionSheetXPath));

		return alerts;
	}
}
