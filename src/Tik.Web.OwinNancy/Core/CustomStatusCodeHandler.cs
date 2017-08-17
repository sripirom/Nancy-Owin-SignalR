using System;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;
using System.Dynamic;
using Tik.Web.OwinNancy.Core.Security;
using Nancy.Security;

namespace Tik.Web.OwinNancy
{
	public class CustomStatusCodeHandler: DefaultViewRenderer, IStatusCodeHandler
	{
		public CustomStatusCodeHandler (IViewFactory factory): base(factory){}

		public bool HandlesStatusCode (Nancy.HttpStatusCode statusCode, 
			Nancy.NancyContext context)
		{
			return statusCode == Nancy.HttpStatusCode.NotFound ||
				statusCode == Nancy.HttpStatusCode.BadRequest ||
				statusCode == Nancy.HttpStatusCode.InternalServerError;
		}

		public void Handle (Nancy.HttpStatusCode statusCode, 
			Nancy.NancyContext context)
		{
			dynamic model = new ExpandoObject ();
			model.IsAuthenticated = context.CurrentUser.IsAuthenticated ();
			model.JournalUser = (JournalUserIdentity)context.CurrentUser;
			var response = RenderView (context, "Error/" + (int)statusCode, model);
			response.StatusCode = statusCode;
			context.Response = response;
		}
	}
}

