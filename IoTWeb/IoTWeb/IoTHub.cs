﻿using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PiOfThings;

namespace IoTWeb

{
	[HubName("IoTHub")]
	public class IoTHub: Hub
	{
		public bool JoinGroup(string groupName)
		{
			try
			{
				Groups.Add (Context.ConnectionId, groupName);
				return true;
			}
			catch	
			{
			}
			return false;
		}

		public void switchOn (GPIOId gpioPinId)
		{
			Console.WriteLine ("Switching PIN");
			Clients.Others.switchOn (gpioPinId);
		}

		public void switchOff (GPIOId gpioPinId)
		{
			Console.WriteLine ("Switching OFF");

			Clients.Others.switchOff (gpioPinId);
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
		}
	}
}