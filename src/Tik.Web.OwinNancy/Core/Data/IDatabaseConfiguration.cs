namespace Tik.Web.OwinNancy.Core.Data
{
	public interface IDatabaseConfiguration {

		void Configure();
		ITransactionWrapper GetTransaction();

		bool IsValid{ get; }

		void Register<T>(T instance) where T: class;
		void Register<T, U>(U instance) where T:class where U: class;
		T Resolve<T>() where T: class;
	}
}

