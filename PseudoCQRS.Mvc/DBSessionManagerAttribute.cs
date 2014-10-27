using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace PseudoCQRS
{
	[AttributeUsage( AttributeTargets.Class, AllowMultiple = false, Inherited = true )]
	public class DbSessionManagerAttribute : ActionFilterAttribute
	{
		public DbSessionManagerAttribute()
		{
			Order = 1;
		}

		public override void OnResultExecuted( ResultExecutedContext filterContext )
		{
			if ( !filterContext.IsChildAction )
				ServiceLocator.Current.GetInstance<IDbSessionManager>().CloseSession();
		}
	}
}