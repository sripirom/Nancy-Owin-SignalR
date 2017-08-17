using System;
using System.Data;
using Tik.Web.OwinNancy.Models.Journal;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Tik.Web.OwinNancy
{
	public class RecentEntryListQuery : IQuery<IList<Entry>>
	{
		public IList<Entry> Execute (IDbConnection conn, IDbTransaction transaction, Guid userId)
		{
			var sql = @"select * from entries where userid = :userid order by entrydate desc limit 20;";

			var result = conn.Query<Entry> (sql, new {userid = userId}, transaction);

			return result.ToList();
		}
	}
}

