using Nancy;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using Nancy.Authentication.Forms;
using Tik.Web.OwinNancy.Core.Security;
using Tik.Web.OwinNancy.Core.Data;
using System.Linq;
using System.Configuration;
using System;
using Nancy.Conventions;
using System.Collections.Generic;
using Nancy.ViewEngines.SuperSimpleViewEngine;
using Npgsql;
using Tik.Web.OwinNancy.AppEngine;

namespace Tik.Web.OwinNancy.Core
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions (NancyConventions conventions)
		{
			conventions.CultureConventions.Insert (0,
				BuiltInCultureConventions.PathCulture);
			base.ConfigureConventions (conventions);
		}

		protected override void ConfigureApplicationContainer (TinyIoCContainer container)
		{
			base.ConfigureApplicationContainer (container);

			container.Register<IEnumerable<ISuperSimpleViewEngineMatcher>>(
				(c, p) =>
				new List<ISuperSimpleViewEngineMatcher> () {
					container.Resolve<TextResourceViewEngineMatcher> ()
				});
		}

		protected override void ConfigureRequestContainer (TinyIoCContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer (container, context);
			container.Register<IJournalUserMapper, InMemoryUserMapper> ();
			container.Register<ITransactionWrapper, TransactionWrapper> ();
		}

		protected override void RequestStartup (TinyIoCContainer container, 
			IPipelines pipelines, NancyContext context)
		{
			var formsAuthConfiguration = new FormsAuthenticationConfiguration () {
				RedirectUrl = "~/signin",
				UserMapper = container.Resolve<IJournalUserMapper> (),
				CryptographyConfiguration = CustomCryptographyConfiguration.Default
			};

			FormsAuthentication.Enable (pipelines, formsAuthConfiguration);

			var connectionString = getHerokuConnectionString() 
				?? ConfigurationManager.ConnectionStrings ["local"].ConnectionString;
			var dbConfig = new TinyIoCDatabaseConfiguration () {
				ConnectionInitializer = () => new NpgsqlConnection (connectionString),
				Container = container,
				Context = context
			};

			UnitOfWork.Enable (pipelines, dbConfig);
		}

		string getHerokuConnectionString ()
		{

			var herokuString = Environment.GetEnvironmentVariable ("DATABASE_URL");
			if (String.IsNullOrEmpty (herokuString)) {
				return null;
			}

			var dbUri = new Uri (herokuString);

			var builder = new NpgsqlConnectionStringBuilder ();
			builder.Username = dbUri.UserInfo.Split (':').First();
			builder.Password = dbUri.UserInfo.Split (':').Last();
			builder.Host = dbUri.Host;
			builder.Port = dbUri.Port;
			builder.Database = dbUri.AbsolutePath.Trim('/');
			builder.SslMode = SslMode.Require;
            builder.SslMode = SslMode.Require;
			return builder.ToString();

		}

		// Since the host already provides a IRootPathProvider
		// implementation, we need to tell Nancy which to use.
		protected override IRootPathProvider RootPathProvider {
			get { return new ServiceRootPathProvider (); }
		}

		#if DEBUG
		protected override NancyInternalConfiguration InternalConfiguration {
			get {
				StaticConfiguration.DisableErrorTraces = false;
				StaticConfiguration.Caching.EnableRuntimeViewUpdates = true;
				StaticConfiguration.Caching.EnableRuntimeViewDiscovery = true;

				return base.InternalConfiguration;
			}
		}
		#endif

	}
}
