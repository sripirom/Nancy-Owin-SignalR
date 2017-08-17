using System.Data;
using System.Transactions;
using System;

namespace Tik.Web.OwinNancy.Core.Data
{
	public class TransactionScopeWrapper : ITransactionWrapper
	{
		IDbConnection connection;
		TransactionScope scope;
		Guid userId;

		public TransactionScopeWrapper (IDbConnection connection)
		{
			this.connection = connection;
		}

		public void Start ()
		{
			scope = new TransactionScope ();
			connection.Open ();
		}

		public void Rollback ()
		{
			Dispose ();
		}

		public void Commit ()
		{
			if (scope != null){
				scope.Complete ();
			}
			Dispose ();
		}

		public void Dispose ()
		{
			if (connection != null){
				connection.Dispose ();
			}

			if (scope != null){
				scope.Dispose ();
			}

			scope = null;
			connection = null;

		}

		public IDbConnection Connection {
			get {
				return connection;
			}
		}

		public IDbTransaction Instance {
			get {
				return null;
			}
		}

		public void SetUserId (Guid userId)
		{
			this.userId = userId;
		}

		public void Execute (ICommand command)
		{
			command.Execute(Connection, Instance, userId);
		}

		public dynamic Execute (IQuery query)
		{
			return query.Execute(Connection, Instance, userId);
		}

		public T Execute<T> (IQuery<T> query)
		{
			return query.Execute(Connection, Instance, userId);
		}
	}
}