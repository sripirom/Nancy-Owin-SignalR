using System;
using Nancy.Authentication.Forms;

namespace Tik.Web.OwinNancy.Core.Security
{
	public interface IJournalUserMapper : IUserMapper
	{
		Guid? ValidateUser (string username, string password);
		Tuple<bool, Guid?> RegisterUser (JournalUserIdentity user);
	}
	
}
