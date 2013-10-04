using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using Microsoft.Practices.Unity;
using PseudoCQRS.Examples.NerdDinner.Infrastructure;

namespace PseudoCQRS.Examples.NerdDinner
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register( GlobalConfiguration.Configuration );
			FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
			RouteConfig.RegisterRoutes( RouteTable.Routes );

			FluentValidationModelValidatorProvider.Configure();
			AutoMapperInitializer.Initialise();
			IUnityContainer container = new UnityContainer();
			UnityRegistrar.Register( container );
			ControllerBuilder.Current.SetControllerFactory( new UnityControllerFactory( container ) );
		}
	}
}