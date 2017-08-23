using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Tik.Web.OwinNancy.Core
{
	public class CustomJsonSerializer : JsonSerializer
	{
		public CustomJsonSerializer ()
		{
			ContractResolver = new SnakeCasePropertyNamesContractResolver ();
		}

	}

	public class SnakeCasePropertyNamesContractResolver : DefaultContractResolver
	{
		public SnakeCasePropertyNamesContractResolver() : base() 
		{
		}

		protected override string ResolvePropertyName(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
				return propertyName;

			var sb = new StringBuilder();
			for (var i = 0; i < propertyName.Length; i++)
			{
				if (i != 0 && char.IsUpper(propertyName[i]))
				{
					sb.Append("_");
				}
				sb.Append(char.ToLower(propertyName[i]));
			}
			return sb.ToString();
		}

	}
}

