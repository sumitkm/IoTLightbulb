using System;
using System.Collections.Generic;

namespace PiOfThings
{
	public static class GPIOPinDirection
	{
		public static string Out = "out";
		public static string In = "in";
	}

	public enum GPIOPinState
	{
		Low = 0,
		High = 1,
		Unknown = 3
	}

	public enum GPIOId
	{
		GPIOUnknown = -1,
		GPIO02 = 2,
		GPIO03 = 3,
		GPIO04 = 4,
		GPIO07 = 7,
		GPIO08 = 8,
		GPIO09 = 9,
		GPIO10 = 10,
		GPIO11 = 11,
		GPIO14 = 14,
		GPIO15 = 15,
		GPIO17 = 17,
		GPIO18 = 18,
		GPIO22 = 22,
		GPIO23 = 23,
		GPIO24 = 24,
		GPIO25 = 25,
		GPIO27 = 27
	}

	public static class GPIOPinMapping
	{
		private static Dictionary<GPIOId, int> GPIOToPin = new Dictionary<GPIOId, int>{
			{ GPIOId.GPIO02, 3 },
			{ GPIOId.GPIO03, 5 },
			{ GPIOId.GPIO04, 4 },
			{ GPIOId.GPIO07, 26 },
			{ GPIOId.GPIO08, 24 },
			{ GPIOId.GPIO09, 21 },
			{ GPIOId.GPIO10, 19 },
			{ GPIOId.GPIO11, 23 },
			{ GPIOId.GPIO14, 8 },
			{ GPIOId.GPIO15, 10 },
			{ GPIOId.GPIO17, 11 },
			{ GPIOId.GPIO18, 12 },
			{ GPIOId.GPIO22, 15 },
			{ GPIOId.GPIO23, 16 },
			{ GPIOId.GPIO24, 18 },
			{ GPIOId.GPIO25, 22 },
			{ GPIOId.GPIO27, 13 }
		};

		private static readonly Dictionary<int, GPIOId> PinToGPIO = new Dictionary<int, GPIOId>{
			{ 1, GPIOId.GPIOUnknown },
			{ 2, GPIOId.GPIOUnknown },
			{ 3, GPIOId.GPIO02 },
			{ 4, GPIOId.GPIO04 },
			{ 5, GPIOId.GPIO03 },
			{ 6, GPIOId.GPIOUnknown },
			{ 7, GPIOId.GPIOUnknown },
			{ 8, GPIOId.GPIO14 },
			{ 9, GPIOId.GPIOUnknown },
			{ 10, GPIOId.GPIO15 },
			{ 11, GPIOId.GPIO17 },
			{ 12, GPIOId.GPIO18 },
			{ 13, GPIOId.GPIO27 },
			{ 14, GPIOId.GPIOUnknown },
			{ 15, GPIOId.GPIO22 },
			{ 16, GPIOId.GPIO23 },
			{ 17, GPIOId.GPIOUnknown },
			{ 18, GPIOId.GPIO24 },
			{ 19, GPIOId.GPIO10 },
			{ 20, GPIOId.GPIOUnknown },
			{ 21, GPIOId.GPIO09 },
			{ 22, GPIOId.GPIO25 },
			{ 23, GPIOId.GPIO11 },
			{ 24, GPIOId.GPIO08 },
			{ 25, GPIOId.GPIOUnknown },
			{ 26, GPIOId.GPIOUnknown },
			{ 27, GPIOId.GPIOUnknown },
			{ 28, GPIOId.GPIOUnknown },
			{ 29, GPIOId.GPIOUnknown },
			{ 30, GPIOId.GPIOUnknown },
			{ 31, GPIOId.GPIOUnknown },
			{ 32, GPIOId.GPIOUnknown },
			{ 33, GPIOId.GPIOUnknown },
			{ 34, GPIOId.GPIOUnknown },
			{ 35, GPIOId.GPIOUnknown },
			{ 36, GPIOId.GPIOUnknown },
			{ 37, GPIOId.GPIOUnknown },
			{ 38, GPIOId.GPIOUnknown },
			{ 39, GPIOId.GPIOUnknown },
			{ 40, GPIOId.GPIOUnknown },
		};

		public static int GetPinNumber (GPIOId gpioNumber)
		{
			return GPIOToPin [gpioNumber];
		}

		public static GPIOId GetGPIOId(int pin)
		{
			if (pin > 0 && pin <= 40) 
			{
				return PinToGPIO [pin];
			} 
			else 
			{
				throw new ArgumentOutOfRangeException ("pin", string.Format ("Invalid pin {0}. Please enter value between 1 and 40 (both inclusive).", pin)); 
			}
		}
	}
}