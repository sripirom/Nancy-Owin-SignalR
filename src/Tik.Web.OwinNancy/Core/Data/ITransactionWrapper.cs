using System.Data;
using System;

namespace Tik.Web.OwinNancy.Core.Data
{
	public interface ITransactionWrapper{
		IDbConnection Connection { get; }
		IDbTransaction Instance { get; }
		void Start();
		void Rollback();
		void Commit();
		void Dispose();
		void SetUserId (Guid identifier);
		void Execute (ICommand command);
		dynamic Execute (IQuery query);
		T Execute<T> (IQuery<T> query);
	}
}