using System;
using Nancy;
using System.Dynamic;
using Tik.Web.OwinNancy.Core.Data;
using Nancy.Security;
using Tik.Web.OwinNancy.Core.Security;

namespace Tik.Web.OwinNancy
{
	public class BaseNancyModule : NancyModule
	{
		public BaseNancyModule () : this("/")
		{
		}

		public BaseNancyModule (string modulePath) : base(modulePath)
		{
			Before += ctx => {
				Model = new ExpandoObject();
				Model.IsAuthenticated = ctx.CurrentUser.IsAuthenticated();
				Model.JournalUser = (JournalUserIdentity)ctx.CurrentUser;
				return null;
			};
		}

		protected dynamic Model { get; private set;}

		protected ITransactionWrapper Transaction{
			get{
				return ((Lazy<ITransactionWrapper>)Context.Items ["wrapper"]).Value;
			}
		}
	}
}

