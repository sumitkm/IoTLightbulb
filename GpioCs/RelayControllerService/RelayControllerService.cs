using System;
using Microsoft.AspNet.SignalR.Client;
using PiOfThings;
using PiOfThings.GpioCore;
using PiOfThings.GpioUtils;
using RelayControllerService.Data;

namespace RelayControllerService
{
	public class RelayControllerService
	{
		readonly GpioManager _manager = new GpioManager ();

		private IHubProxy IoTHub { get; set; }

		private HubConnection IoTHubConnection { get; set; }

		public RelayControllerService (string url)
		{
			IoTHubConnection = new HubConnection (url);
			IoTHub = IoTHubConnection.CreateHubProxy ("IoTHub");

			IoTHub.On<GpioId> ("SwitchOn", OnSwitchedOn);

			IoTHub.On<GpioId> ("SwitchOff", OnSwitchedOff);

			Console.Read ();
		}

		private void OnSwitchedOn (GpioId gpioPinId)
		{
			Console.WriteLine ("SWITCH ON RECIEVED " + gpioPinId);
			if (_manager.CurrentPin != gpioPinId) {
				_manager.SelectPin (gpioPinId);
			} 
			_manager.WriteToPin (GpioPinState.Low);
		}

		private void OnSwitchedOff (GpioId gpioPinId)
		{
			Console.WriteLine ("SWITCH OFF RECIEVED " + gpioPinId);

			if (_manager.CurrentPin != gpioPinId) {
				_manager.SelectPin (gpioPinId);
			} 
			_manager.WriteToPin (GpioPinState.High);
		}

		public void StartConnection ()
		{
			//Start connection
			DeviceDataContext context = new DeviceDataContext ();
			DeviceData data = context.GetActiveDeviceData ();
			IoTHubConnection.Start ().ContinueWith (task => {
				if (task.IsFaulted) 
				{
					Console.WriteLine ("There was an error opening the connection:{0}",
						task.Exception.GetBaseException ());
				} 
				else 
				{
					Console.WriteLine ("Connected");

					IoTHub.Invoke<string> ("HandShake", data.DeviceId).ContinueWith (joinGroupTask => {
						if (task.IsFaulted) 
						{
							Console.WriteLine ("There was an error calling send: {0}",
								task.Exception.GetBaseException ());
						} 
						else 
						{
							Console.WriteLine ("Handshake successful - " + joinGroupTask.Result);
						}
					});
				}

			}).Wait ();
		}

		public void StopConnection ()
		{
			_manager.ReleaseAll ();
			IoTHubConnection.Stop ();
		}
	}
}
