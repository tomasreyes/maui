﻿using System;
using Microsoft.Maui.Controls;

namespace Maui.Controls.Sample.Pages
{
	public partial class HybridWebViewPage
	{
		public HybridWebViewPage()
		{
			InitializeComponent();
		}

		private void SendMessageButton_Pressed(object sender, EventArgs e)
		{
			//hwv.SendMessage()...
		}

		private void hwv_RawMessageReceived(object sender, HybridWebView.HybridWebViewRawMessageReceivedEventArgs e)
		{
			statusLabel.Text += e.Message;
		}
	}
}
