﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues
{
	public class Issue2929 : _IssuesUITest
	{
		const string Success = "Success";
		const string Go = "Go";

		public Issue2929(TestDevice testDevice) : base(testDevice)
		{
		}

		public override string Issue => "[UWP] ListView with null ItemsSource crashes on 3.0.0.530893";
	
		[Test]
		[Category(UITestCategories.ListView)]
		[Category(UITestCategories.Compatibility)]
		[FailsOnIOS]
		public void NullItemSourceDoesNotCrash()
		{
			this.IgnoreIfPlatforms([TestDevice.Android, TestDevice.Mac]);

			// If we can see the Success label, it means we didn't crash. 
			App.WaitForElement(Success);
		}

		[Test]
		[Category(UITestCategories.ListView)]
		[Category(UITestCategories.Compatibility)]
		[FailsOnIOS]
		[FailsOnWindows]
		public void SettingItemsSourceToNullDoesNotCrash()
		{
			this.IgnoreIfPlatforms([TestDevice.Android, TestDevice.Mac]);
			
			App.WaitForElement(Go);
			App.Tap(Go);

			// If we can see the Success label, it means we didn't crash. 
			App.WaitForElement(Success);
		}
	}
}