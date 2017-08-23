using System;
using Nancy;
using Nancy.Security;
using Tik.Web.OwinNancy.Models.Journal;
using Nancy.ModelBinding;
using Tik.Web.OwinNancy.Core.Data;
using Nancy.Extensions;
using System.Dynamic;

namespace Tik.Web.OwinNancy
{
	public class JournalModule : BaseNancyModule
	{
		public JournalModule () : base("/journal")
		{
			this.RequiresAuthentication ();
			Get ["/"] = _ => {

				var query = new RecentEntryListQuery();
				Model.RecentEntries = Transaction.Execute(query);

				return View ["index", Model];
			};


			Post ["/"] = _ => {
				var entry = this.Bind<Entry>();

				var command = new EntryCreateCommand (entry);

				var result = Transaction.Execute (command);


				if (result) {
					if (Request.IsAjaxRequest ()) {
						return HttpStatusCode.Created;
					} else {
						return  Response.AsRedirect ("~/journal/", Nancy.Responses.RedirectResponse.RedirectType.SeeOther);
					}
				} else {
					return HttpStatusCode.InternalServerError;
				}
			};
		}
	}
}

