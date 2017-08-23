using System;
using System.Data;
using Nancy;
using System.Collections.Generic;

namespace Tik.Web.OwinNancy.Core.Data
{

	public abstract class BaseDatabaseConfiguration<TContainer> : IDatabaseConfiguration{

		public TContainer Container { get; set; }

		public NancyContext Context {get; set;}

		public Func<IDbConnection> ConnectionInitializer { get; set; }

		public void Configure()
		{
			Register<IDbConnection> (ConnectionInitializer.Invoke());
			var wrapper = new Lazy<ITransactionWrapper> (() =>  {
				var transaction = Resolve<ITransactionWrapper> ();
				transaction.Start ();
				return transaction;
			});
			Register <Lazy<ITransactionWrapper>> (wrapper);
			Context.Items.Add (new KeyValuePair<string, object> ("wrapper", wrapper));
		}

		public ITransactionWrapper GetTransaction(){
			var lazyTransaction = Resolve<Lazy<ITransactionWrapper>>();
			return lazyTransaction.IsValueCreated ? lazyTransaction.Value : null;
		}
			
		public bool IsValid {
			get {
				return ConnectionInitializer != null;
			}
		}

		public abstract void Register<T> (T instance) where T: class;
		public abstract void Register<T, U> (U instance) where T:class where U: class;
		public abstract T Resolve<T> () where T: class;
	}
}

