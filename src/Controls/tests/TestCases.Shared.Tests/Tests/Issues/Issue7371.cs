﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues
{
	public class Issue7371 : _IssuesUITest
	{
		public Issue7371(TestDevice testDevice) : base(testDevice)
		{
		}

		public override string Issue => "iOS race condition(or not checking for null) of refreshing(offset animation) causes NullReferenceException";

		[Test]
		[Category(UITestCategories.RefreshView)]
		[Category(UITestCategories.Compatibility)]
		[FailsOnAndroid]
		public async Task RefreshingListViewCrashesWhenDisposedTest()
		{
			this.IgnoreIfPlatforms([TestDevice.Mac, TestDevice.Windows]);

			await Task.Delay(500);
			App.WaitForElement("Success");
		}
	}
}