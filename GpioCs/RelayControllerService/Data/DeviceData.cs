using System;
using Mono.Data.Sqlite;

namespace RelayControllerService
{
	public class DeviceData
	{
		public DeviceData ()
		{
		}

		public int Id
		{
			get;
			set;
		}

		public string DeviceId
		{
			get;
			set;
		}

		public Boolean IsActive
		{
			get;
			set;
		}
	}
}

