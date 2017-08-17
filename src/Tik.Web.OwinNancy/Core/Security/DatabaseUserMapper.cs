using System;
using Tik.Web.OwinNancy.Core.Data;
using Dapper;
using System.Linq;
using Nancy.Security;

namespace Tik.Web.OwinNancy.Core.Security
{
	public class DatabaseUserMapper : IJournalUserMapper
	{
		Lazy<ITransactionWrapper> lazyTransaction;

		public DatabaseUserMapper (Lazy<ITransactionWrapper> lazyTransaction)
		{
			this.lazyTransaction = lazyTransaction;
		}

		ITransactionWrapper GetTransaction(){
			return lazyTransaction.Value;
		}

		public IUserIdentity GetUserFromIdentifier (Guid identifier, Nancy.NancyContext context)
		{

			var transaction = GetTransaction ();

			var sql = "select * from users where id=:id;";

			var user = transaction.Connection
				.Query<JournalUserIdentity>(
					sql, 
					new { id = identifier }, 
					transaction.Instance).
				FirstOrDefault();

			if (user != null){
				transaction.SetUserId (identifier);
			}

			return user;
		}

		public Guid? ValidateUser (string username, string password)
		{
			var transaction = GetTransaction ();

			var sql = "select * from users where username=:username or email=:username;";

			var user = transaction.Connection
				.Query(
					sql, 
					new { username }, 
					transaction.Instance)
				.FirstOrDefault();

			if (user != null && password.Validate((string)user.password)){
				return user.id;
			}

			return null;
		}

		public Tuple<bool, Guid?> RegisterUser (JournalUserIdentity user)
		{

			var transaction = GetTransaction ();

			var existingSql = "select * from users where username=:username or email=:email;";

			var existing = transaction.Connection
				.Query (
					existingSql, 
					new { username = user.UserName, email = user.Email}, 
					transaction.Instance)
				.FirstOrDefault ();

			if (existing != null){
				return new Tuple<bool, Guid?> (false, null);
			}

			var insertSql = @"
				insert into users (id, username, name, email, password)
				values (:id, :username, :name, :email, :password);
				";
			var id = Guid.NewGuid ();
			var encryptedPassword = user.Password.BCrypt ();
			var result = transaction.Connection
				.Execute (
		             insertSql,
		             new {
							id = id,
							username = user.UserName, 
							name = user.Name, 
							email = user.Email,
							password = encryptedPassword
						}, 
					transaction.Instance);

			return result > 0 
				? new Tuple<bool, Guid?> (true, id) 
				: new Tuple<bool, Guid?> (false, null);
		}
	}
}

