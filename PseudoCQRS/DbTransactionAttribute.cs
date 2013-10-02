using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace PseudoCQRS
{
	[AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = false )]
	public class DbTransactionAttribute : ActionFilterAttribute
	{

		public DbTransactionAttribute()
		{
			Order = 2;
		}

		public override void OnActionExecuting( ActionExecutingContext filterContext )
		{
			ServiceLocator.Current.GetInstance<IDbSessionManager>().OpenTransaction();
		}

		public override void OnResultExecuted( ResultExecutedContext filterContext )
		{
			ServiceLocator.Current.GetInstance<IDbSessionManager>().CommitTransaction();
		}
	}
}
