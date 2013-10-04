using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace PseudoCQRS.Examples.NerdDinner.Infrastructure
{
	public class UnityControllerFactory : DefaultControllerFactory
	{
		private readonly IUnityContainer _container;

		public UnityControllerFactory( IUnityContainer container )
		{
			_container = container;
		}


		protected override IController GetControllerInstance( System.Web.Routing.RequestContext requestContext, Type controllerType )
		{
			if ( controllerType == null )
				return base.GetControllerInstance( requestContext, controllerType );

			var controller = (Controller)_container.Resolve( controllerType );

			return controller;
		}
	}
}