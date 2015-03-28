using System;
using Microsoft.AspNet.SignalR.Client;
using PiOfThings;

namespace RelayControllerService
{
	public class RelayControllerService
	{
		readonly GPIOManager _manager = new GPIOManager();

		private IHubProxy IoTHub { get; set; }
		private HubConnection IoTHubConnection { get; set; }

		public RelayControllerService (string url)
		{
			IoTHubConnection = new HubConnection (url);
			IoTHub = IoTHubConnection.CreateHubProxy("IoTHub");

			IoTHub.On<GPIOId>("switchOn", OnSwitchedOn);

			IoTHub.On<GPIOId>("switchOff", OnSwitchedOff);

			Console.Read();
		}

		private void OnSwitchedOn(GPIOId gpioPinId)
		{
			Console.WriteLine("SWITCH ON RECIEVED " + gpioPinId);
			if (_manager.CurrentPin != gpioPinId)
			{
				_manager.SelectPin (gpioPinId);
				_manager.WriteToPin (GPIOPinState.Low);
			}
			else
			{
				_manager.WriteToPin (GPIOPinState.Low);
			}
		}

		private void OnSwitchedOff(GPIOId gpioPinId)
		{
			Console.WriteLine("SWITCH OFF RECIEVED " + gpioPinId);

			if (_manager.CurrentPin != gpioPinId)
			{
				_manager.SelectPin (gpioPinId);
				_manager.WriteToPin (GPIOPinState.High);
			}
			else
			{
				_manager.WriteToPin(GPIOPinState.High);
			}
		}

		public void StartConnection()
		{
			//Start connection
			IoTHubConnection.Start().ContinueWith(task => {
				if (task.IsFaulted) 
				{
					Console.WriteLine("There was an error opening the connection:{0}",
						task.Exception.GetBaseException());
				} else {
					Console.WriteLine("Connected");

					IoTHub.Invoke<string>("JoinGroup", "homePi").ContinueWith(joinGroupTask => {
						if (task.IsFaulted) 
						{
							Console.WriteLine("There was an error calling send: {0}",
								task.Exception.GetBaseException());
						} 
						else 
						{
							Console.WriteLine(joinGroupTask.Result);
						}
					});
				}

			}).Wait();
		}

		public void StopConnection()
		{
			_manager.ReleaseAll ();
			IoTHubConnection.Stop();
		}
	}
}
