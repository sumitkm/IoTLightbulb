using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PiOfThings.GpioUtils;

namespace IoTWeb

{
	[HubName("IoTHub")]
	public class IoTHub: Hub
	{
		public string Handshake(string deviceId)
		{
			return deviceId;
		}

		public void SwitchOn (GpioId gpioPinId)
		{
			Console.WriteLine ("Switching PIN" + gpioPinId.ToString("D"));
			Clients.Others.switchOn (gpioPinId);
		}

		public void SwitchOff (GpioId gpioPinId)
		{
			Console.WriteLine ("Switching OFF " + gpioPinId.ToString("D"));
			Clients.Others.switchOff (gpioPinId);
		}
	}
}
