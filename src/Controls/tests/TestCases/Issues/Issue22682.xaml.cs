﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Maui.Controls.Sample.Issues
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[Issue(IssueTracker.Github, 22682, "[iOS] FontIconSource crash", PlatformAffected.iOS)]
	public partial class Issue22682 : ContentPage
	{
		public Issue22682()
		{
			InitializeComponent();
		}
	}
}