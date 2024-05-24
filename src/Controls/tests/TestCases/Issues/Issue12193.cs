﻿using System.Collections.Generic;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;

namespace Maui.Controls.Sample.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 12193, "[Bug] CarouselView content disappears after 2 rotations if TextType=Html is used",
		PlatformAffected.iOS)]
	public class Issue12193 : TestContentPage
	{
		public const string HTML = "HTML";

		protected override void Init()
		{
			Title = "CarouselView HTML Label";

			var instructions = new Label { Text = $"Rotate the device, then rotate it back 3 times. If the label \"{HTML}\" disappears, this test has failed." };

			var source = new List<string>();
			for (int n = 0; n < 10; n++)
			{
				source.Add($"Item: {n}");
			}

			var template = new DataTemplate(() =>
			{
				var label = new Label
				{
					TextType = TextType.Html,
					Text = $"<p style='background-color:red;'>{HTML}</p>",
					AutomationId = HTML
				};

				return label;
			});

			var cv = new CarouselView()
			{
				ItemsSource = source,
				ItemTemplate = template,
				Loop = false
			};

			var layout = new StackLayout();

			layout.Children.Add(instructions);
			layout.Children.Add(cv);

			Content = layout;
		}
	}
}