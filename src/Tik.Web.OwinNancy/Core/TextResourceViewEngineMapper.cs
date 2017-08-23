using System;
using System.Text.RegularExpressions;
using Nancy.Localization;
using Nancy.ViewEngines.SuperSimpleViewEngine;
using Nancy;

namespace Tik.Web.OwinNancy.Core
{
	// based on http://stackoverflow.com/a/23614404/119562
	internal sealed class TextResourceViewEngineMatcher :
	ISuperSimpleViewEngineMatcher
	{
		/// <summary>
		///   Compiled Regex for translation substitutions.
		/// </summary>
		private readonly Regex ResourceRegEx;
		private readonly ITextResource resource;

		public TextResourceViewEngineMatcher (ITextResource resource)
		{
			this.resource = resource;

			// This regex will match strings like:
			// @Text.Resources.Hello_World
			// @Text.Resources.FooBarBaz;
			ResourceRegEx =
				new Regex (
					@"@Text\.(?<Key>[a-zA-Z0-9-_\.]+);?",
					RegexOptions.Compiled);
		}

		public string Invoke (string content, dynamic model, IViewEngineHost host)
		{
			var Finder = new TextResourceFinder (resource, 
				(NancyContext)host.Context);
			return ResourceRegEx.Replace (
				content,
				m => {
					var key = m.Groups ["Key"].Value;
					return Finder [key];
				});
		}
	}
}

