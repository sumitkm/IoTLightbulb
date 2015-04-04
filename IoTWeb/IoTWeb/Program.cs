using System;
using Microsoft.Owin.Hosting;

namespace IoTWeb
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string baseUrl = "http://localhost:5000";
			using (WebApp.Start<IoTStartup>(baseUrl))
			{
				Console.WriteLine("Press Enter to quit.");
				Console.ReadKey();
			}
		}
	}
}
