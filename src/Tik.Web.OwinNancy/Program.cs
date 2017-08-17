using System;
using Microsoft.Owin.Hosting;
using Tik.Web.OwinNancy.AppEngine;
using Topshelf;

namespace Tik.Web.OwinNancy
{
    class MainClass
    {

        private static string serviceName = "Tik.Web.OwinNancy";

		public static int Main(string[] args)
		{
            
			var exitCode = HostFactory.Run(x =>
			{
				log4net.Config.XmlConfigurator.Configure();
                x.UseLinuxIfAvailable();
				x.Service<TopshelfServiceControl>();
				x.RunAsLocalSystem();
				x.SetDescription(string.Format("Owin + signalR as Owin service http://{0}:{1}", Env.Get.HostAddress , Env.Get.HostPort));
				x.SetDisplayName(serviceName);
				x.SetServiceName(serviceName);
                x.StartAutomatically();
                //x.RunAsLocalSystem();
			});




			return (int)exitCode;
		}

		public class TopshelfServiceControl : ServiceControl
		{

			private IDisposable _webApplication;


			public TopshelfServiceControl()
			{
			}

			public bool Start(HostControl hostControl)
			{

				_webApplication = WebApp.Start<Startup>(string.Format("http://{0}:{1}", Env.Get.HostAddress, Env.Get.HostPort));
				return true;
			}

			public bool Stop(HostControl hostControl)
			{
				_webApplication.Dispose();
				return true;
			}
		}
    }
	public sealed class Env
	{
		private static volatile Env instance;
		private static object syncRoot = new Object();

		private Env() { }

        public string HostAddress { get { return "localhost"; } }// GetEnv("%HostAddress%"); } }
        public string HostPort { get { return "8005"; } }// GetEnv("%HostPort%"); } }

		private string GetEnv(string name)
		{
			return Environment.ExpandEnvironmentVariables(name);
		}

		public static Env Get
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new Env();
					}
				}

				return instance;
			}
		}
	}
}
