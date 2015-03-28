using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(IoTWeb.IoTStartup))]

namespace IoTWeb
{
	public class IoTStartup
	{
		public IoTStartup ()
		{
		}

		public void Configuration(IAppBuilder app)
		{
			app.UseCors (CorsOptions.AllowAll);
			app.MapSignalR ();		
			string exeFolder = System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().Location);
			string webFolder = System.IO.Path.Combine (exeFolder, "Web");
			Console.WriteLine ("Hosting Files from : " + webFolder);
			app.UseStaticFiles ("/Web");
		}
	}
}

