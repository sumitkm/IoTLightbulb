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
			Console.WriteLine ("Hosting Files from : /Web");
			app.UseStaticFiles ("/Web");
		}
	}
}

