﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues
{
	public class Issue12777 : _IssuesUITest
	{
		public Issue12777(TestDevice testDevice) : base(testDevice)
		{
		}

		public override string Issue => "[Bug] CarouselView NRE if item template is not specified";

		[Test]
		[Category(UITestCategories.CarouselView)]
		[Category(UITestCategories.Compatibility)]
		[FailsOnIOS]
		public void Issue12777Test()
		{
			this.IgnoreIfPlatforms([TestDevice.Mac, TestDevice.Windows]);

			App.WaitForElement("TestCarouselView");
			App.Screenshot("Test passed");
		}
	}
}