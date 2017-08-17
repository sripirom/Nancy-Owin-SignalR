using System;
using Tik.Web.OwinNancy.Models.Journal;
using Dapper;
using System.Data;

namespace Tik.Web.OwinNancy
{
	public class EntryCreateCommand : IQuery<bool>
	{
		readonly Entry entry;

		public EntryCreateCommand (Entry entry)
		{
			this.entry = entry;
		}

		public bool Execute(IDbConnection connection, IDbTransaction transaction, Guid userId){

			var sql = @"insert into entries (entrydate, userid, description, hungerlevel, enjoyment)
				values (:entrydate, :userid, :description, :hungerlevel, :enjoyment);";

			var result = connection.Execute (
				sql, 
				new { 
					entrydate = entry.EntryDate,
					userid = userId,
					description = entry.Description,
					hungerlevel = (int) entry.HungerLevel,
					enjoyment = entry.Enjoyment
				},
				transaction);
			return result > 0;
		}
	}
}

