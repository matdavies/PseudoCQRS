using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using Microsoft.Practices.Unity;
using PseudoCQRS.Examples.NerdDinner.Infrastructure;

namespace PseudoCQRS.Examples.NerdDinner
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
			RouteConfig.RegisterRoutes( RouteTable.Routes );

			FluentValidationModelValidatorProvider.Configure();
			IUnityContainer container = new UnityContainer();
			UnityRegistrar.Register( container );

			AutoMapperInitializer.Initialise();

			ControllerBuilder.Current.SetControllerFactory( new UnityControllerFactory( container ) );
			DummyDataCreator.PopulateRepository();
		}
	}
}