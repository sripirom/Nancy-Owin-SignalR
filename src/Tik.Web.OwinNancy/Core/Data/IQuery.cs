using System;
using System.Data;

namespace Tik.Web.OwinNancy
{
	public interface ICommand
	{
		void Execute (IDbConnection conn, IDbTransaction transaction, Guid userId);
	}

	public interface IQuery<out T>
	{
		T Execute (IDbConnection conn, IDbTransaction transaction, Guid userId);
	}

	public interface IQuery
	{
		dynamic Execute (IDbConnection conn, IDbTransaction transaction, Guid userId);
	}
}


