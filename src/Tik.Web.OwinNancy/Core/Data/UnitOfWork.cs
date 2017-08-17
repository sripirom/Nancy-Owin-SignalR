using System;
using Nancy.Bootstrapper;
using Tik.Web.OwinNancy.Core.Data;
using Nancy;

namespace Tik.Web.OwinNancy.Core.Data
{
	public static class UnitOfWork
	{			
		public static void Enable(IPipelines pipelines, IDatabaseConfiguration config){
			if (pipelines == null){
				throw new ArgumentNullException("pipelines");
			}

			if (config == null){
				throw new ArgumentNullException("config");
			}

			if (!config.IsValid){
				throw new InvalidOperationException ("Configuration is not valid");
			}
				
			config.Configure ();

			pipelines.AfterRequest.AddItemToEndOfPipeline(ctx => {
				var transaction = config.GetTransaction();
				if (transaction != null)
				{
					if (ctx.Response.StatusCode >= HttpStatusCode.BadRequest)
					{
						transaction.Rollback();
					} else {
						transaction.Commit();
					}
				}
			});

			pipelines.OnError.AddItemToEndOfPipeline((ctx, e) => {
				var transaction = config.GetTransaction();
				if (transaction != null)
				{
					transaction.Rollback();
				}
				return ctx.Response;
			});
		}
	}
}

