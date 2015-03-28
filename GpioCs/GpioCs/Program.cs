using System;
using System.IO;
using PiOfThings;

namespace GpioCs
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