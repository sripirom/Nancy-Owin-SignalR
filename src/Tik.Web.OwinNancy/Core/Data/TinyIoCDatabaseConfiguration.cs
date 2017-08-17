using Nancy.TinyIoc;

namespace Tik.Web.OwinNancy.Core.Data
{
	public class TinyIoCDatabaseConfiguration : BaseDatabaseConfiguration<TinyIoCContainer>
	{
		public override void Register<T> (T instance)
		{
			Container.Register(instance);
		}

		public override void Register<T, U> (U instance) 
		{
			Container.Register(instance);
		}

		public override T Resolve<T> () 
		{
			return Container.Resolve<T> ();
		}
	}
}

