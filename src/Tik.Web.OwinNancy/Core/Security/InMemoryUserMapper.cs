using System;
using System.Collections.Generic;
using Nancy.Security;
using Nancy;
using System.Linq;

namespace Tik.Web.OwinNancy.Core.Security
{
	public class InMemoryUserMapper : IJournalUserMapper
	{
		private static readonly IDictionary<Guid, JournalUserIdentity> users = new Dictionary<Guid, JournalUserIdentity> ();

		static InMemoryUserMapper ()
		{
			users.Add (
				new Guid ("caa2baae-a546-4e42-a75d-f3f776373a83"), 
				new JournalUserIdentity () {
					UserName = "pichit",
					Name = "Pichit",
					Email = "pichit@sripirom.com", 
					Password = "password".BCrypt()
				}
			);
		}

		public IUserIdentity GetUserFromIdentifier (Guid identifier, NancyContext context)
		{
			return users.FirstOrDefault (u => u.Key == identifier).Value;
		}

		public Guid? ValidateUser (string username, string password)
		{
			var userRecord = users.FirstOrDefault (u => 
				(String.Equals (u.Value.UserName, username, StringComparison.CurrentCultureIgnoreCase)
			                 || String.Equals (u.Value.Email, username, StringComparison.CurrentCultureIgnoreCase))
				&& password.Validate(u.Value.Password));

			return userRecord.Key;

		}

		public Tuple<bool, Guid?> RegisterUser (JournalUserIdentity user)
		{
			if (new List<JournalUserIdentity>(users.Values).Exists (u => 
				String.Equals (u.UserName, user.UserName, StringComparison.CurrentCultureIgnoreCase)
				||String.Equals (u.Email, user.Email, StringComparison.CurrentCultureIgnoreCase))) {
				return new Tuple<bool, Guid?> (false, null);
			}
			user.Password = user.Password.BCrypt ();
			var guid = Guid.NewGuid ();
			users.Add (guid, user);
			return new Tuple<bool, Guid?> (true, guid);
		}
	}
}

