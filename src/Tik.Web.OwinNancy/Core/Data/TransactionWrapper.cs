using System.Data;
using System;

namespace Tik.Web.OwinNancy.Core.Data
{
	public class TransactionWrapper : ITransactionWrapper
	{
		IDbConnection connection;
		IDbTransaction transaction;
		Guid userId;

		public TransactionWrapper (IDbConnection connection)
		{
			this.connection = connection;
		}

		public void Start ()
		{
			connection.Open ();
			transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		public void Rollback ()
		{
			if (transaction != null){
				transaction.Rollback ();
			}
			Dispose ();
		}

		public void Commit ()
		{
			if (transaction != null){
				transaction.Commit ();
			}
			Dispose ();
		}

		public void Dispose ()
		{
			if (transaction != null){
				transaction.Dispose ();
			}

			if (connection != null){
				connection.Dispose ();
			}

			transaction = null;
			connection = null;

		}

		public IDbConnection Connection {
			get {
				return connection;
			}
		}

		public IDbTransaction Instance {
			get {
				return transaction;
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