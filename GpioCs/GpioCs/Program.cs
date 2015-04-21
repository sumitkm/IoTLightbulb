using System;
using System.IO;
using PiOfThings;

namespace PiOfThings.Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try
			{
				GPIOManager gpioManager = new GPIOManager();
				gpioManager.SelectPin(GPIOId.GPIO17);
				gpioManager.WriteToPin(GPIOPinState.Low);
				Console.ReadLine();
				GPIOPinState state = gpioManager.ReadFromPin(gpioManager.CurrentPin);
				Console.WriteLine("Current Pin 17: " + state);

				gpioManager.SelectPin(GPIOId.GPIO22);
				gpioManager.WriteToPin(GPIOPinState.Low);
				Console.ReadLine();
				GPIOPinState state22 = gpioManager.ReadFromPin(gpioManager.CurrentPin);
				Console.WriteLine("Current Pin 22: " + state22);

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

				Console.WriteLine("Press enter to close!");
				Console.ReadLine();
				gpioManager.ReleasePin(GPIOId.GPIO17);
				gpioManager.ReleasePin(GPIOId.GPIO22);
				Console.WriteLine ("Completed without errors");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}