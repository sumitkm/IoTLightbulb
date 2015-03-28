using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(IoTLightbulbService.RelayControllerStartup))]
namespace IoTLightbulbService
{
	public class RelayControllerStartup
	{
		public RelayControllerStartup ()
		{
		}

		public void Configuration(IAppBuilder builder)
		{
			builder.UseCors (CorsOptions.AllowAll);
			builder.MapSignalR ();
		}
	}
}