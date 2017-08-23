using System;
using System.Data;

namespace Tik.Web.OwinNancy
{
	public interface ICommand
	{
		bool Execute (IDbConnection conn, IDbTransaction transaction, Guid userId);
	}

	public interface ICommand<out T>
	{
		T Execute (IDbConnection conn, IDbTransaction transaction, Guid userId);
	}
}

