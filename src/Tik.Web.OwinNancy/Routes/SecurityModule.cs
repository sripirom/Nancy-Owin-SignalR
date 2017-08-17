using Nancy;
using System;
using Nancy.Authentication.Forms;
using Nancy.Extensions;
using Nancy.ModelBinding;
using System.Dynamic;
using Tik.Web.OwinNancy.Core.Security;

namespace Tik.Web.OwinNancy.Routes
{

	public class SecurityModule : BaseNancyModule
	{
		public SecurityModule (IJournalUserMapper mapper)
		{

			Get ["/signin"] = _ => {
				if (Model.IsAuthenticated){
					return Context.GetRedirect("~/journal");
				}

				Model.Error = Request.Query.error.HasValue;
				Model.ErrorHeading = "Authorization failed.";
				Model.ErrorMessage = "Please check your username and password or <a href=\"@Path['~/register']\">Register</a> for a new account.";
				return View ["signin", Model];
			};

			Post ["/signin"] = _ => {
				Guid? userGuid = mapper.ValidateUser ((string)this.Request.Form.username, (string)this.Request.Form.password);

				if (userGuid == null || userGuid == Guid.Empty) {
					return this.Context.GetRedirect ("~/signin?error=true&username=" + (string)this.Request.Form.username);
				}

				DateTime? expiry = null;
				if (this.Request.Form.RememberMe.HasValue) {
					expiry = DateTime.Now.AddDays (7);
				}

				return this.LoginAndRedirect (userGuid.Value, expiry, "~/journal");
			};

			Get ["/register"] = _ => {
				if (Model.IsAuthenticated){
					return Context.GetRedirect("~/journal");
				}
				return View ["register"];
			};

			Post ["/register"] = _ => {
				var user = this.Bind<JournalUserIdentity>();
				var success = mapper.RegisterUser (user);
				return !success.Item1 
					? this.Context.GetRedirect ("~/register?error=true") 
					: this.Login ((Guid)success.Item2, null, "~/journal");

			};

			Get["/signout"] = _ => this.Logout ("~/");
		}
	}

}
