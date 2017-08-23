using System.Collections.Generic;
using Nancy.Security;

namespace Tik.Web.OwinNancy.Core.Security
{

	public class JournalUserIdentity : IUserIdentity
	{
		public string UserName { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public IEnumerable<string> Claims { get; set; }
	}
}

