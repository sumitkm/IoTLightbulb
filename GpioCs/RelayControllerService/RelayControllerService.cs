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
			IoTHub = IoTHubConnection.CreateHubProxy ("PiotHub");

			IoTHub.On<GpioId> ("SwitchOn", OnSwitchedOn);

			IoTHub.On<GpioId> ("SwitchOff", OnSwitchedOff);

			IoTHub.On ("StatusProbe", OnProbeRecieved);

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

		private void OnProbeRecieved()
		{
			Console.WriteLine ("StatusProbe RECIEVED ");
			try
			{
			DeviceStateData data = new DeviceStateData ();
			for (int i = 1; i <= 40; i++) 
			{
				GpioId currentPinId = GpioPinMapping.GetGPIOId (i);
				if (currentPinId != GpioId.GPIOUnknown) 
				{
					//_manager.SelectPin (GpioPinMapping.GetGPIOId (i));
					GpioPinState state = _manager.ReadFromPin (currentPinId);
					data.GpioPinStates.Add (currentPinId, state);
					//_manager.ReleasePin (currentPinId);
				}
			}
			data.TimeStamp = DateTime.UtcNow.Ticks;
			IoTHub.Invoke<string> ("CurrentStatus", data).ContinueWith (sendStatusTask => 
				{
					if(sendStatusTask.IsFaulted)
					{
						Console.WriteLine ("There was an error opening the connection:{0}",
							sendStatusTask.Exception.GetBaseException ());
					}
					else
					{
						Console.WriteLine("Probe data sent: {0}" + new DateTime(data.TimeStamp).ToLongDateString());
					}
				});
			}
			catch (Exception ex) 
			{
				Console.WriteLine ("Exception : {0}" + ex.Message);
			}

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
