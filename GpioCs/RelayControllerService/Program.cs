using System;
using PiOfThings;
using RelayControllerService;

namespace RelayControllerService
{
	class MainClass
	{
		static string url = "http://localhost:5000/";

		static void Main(string[] args)
		{
			if (args.Length > 0 && args [0] == "-url")
			{
				if (args.Length > 2)
				{
					url = args [1];
				}
			}
			RelayControllerService service = new RelayControllerService (url);
			service.StartConnection ();
			Console.WriteLine ("Press any key to stop service");
			Console.Read ();
			service.StopConnection ();
		}
	}
}
