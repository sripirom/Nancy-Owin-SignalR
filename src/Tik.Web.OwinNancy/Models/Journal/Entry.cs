using System;
using Tik.Web.OwinNancy.Core.Security;

namespace Tik.Web.OwinNancy.Models.Journal
{
	public class Entry
	{
		public int Id { get; set; }
		public DateTime EntryDate { get; set; }
		public string Description { get; set; }
		public HungerLevel HungerLevel { get; set; }
		public int Enjoyment { get; set; }
	}
}

