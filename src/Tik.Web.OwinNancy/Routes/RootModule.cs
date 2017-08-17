using System;
using Nancy;

namespace Tik.Web.OwinNancy
{
	public class RootModule : BaseNancyModule
	{
		public RootModule ()
		{
			Get ["/"] = _ => View ["landing", Model];
		}
	}
}
