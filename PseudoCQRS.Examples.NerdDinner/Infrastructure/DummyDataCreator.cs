using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using PseudoCQRS.Examples.NerdDinner.Entities;

namespace PseudoCQRS.Examples.NerdDinner.Infrastructure
{
	public static class DummyDataCreator
	{
		public static void PopulateRepository()
		{
			var repository = ServiceLocator.Current.GetInstance<IRepository>();
			repository.Save( new User( "Robert Martin", "robert", "robert", true ) );
			repository.Save( new User( "Uncle Bob", "bob", "bob", false ) );
			repository.Save( new User( "Peter Pan", "peter", "peter", false ) );
		}
	}
}