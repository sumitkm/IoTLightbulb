using System;
using System.Collections.Generic;
using PiOfThings.GpioUtils;

namespace RelayControllerService
{
	public class DeviceStateData
	{
		public Dictionary<GpioId, GpioPinState> GpioPinStates 
		{
			get;
			set;
		}

		public long TimeStamp {
			get;
			set;
		}

		public DeviceStateData ()
		{
			GpioPinStates = new Dictionary<GpioId, GpioPinState> ();

		}
	}
}

