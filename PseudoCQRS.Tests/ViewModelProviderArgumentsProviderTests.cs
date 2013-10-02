using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;
using Rhino.Mocks;

namespace PseudoCQRS.Tests
{
	public class ViewModelProviderArgumentsProviderTests
	{

		[Test]
		public void CanFillArgument()
		{
			var propertyValueProviderFactory = MockRepository.GenerateMock<IPropertyValueProviderFactory>();
			var propertyValueProviders = new List<IPropertyValueProvider>
			{
				new TestPropertyValueProvider()
			};
			propertyValueProviderFactory.Stub( x => x.GetPropertyValueProviders() ).Return( propertyValueProviders );
			var viewModelProviderArgumentsProvider = new ViewModelProviderArgumentsProvider( propertyValueProviderFactory );
			var arguments = viewModelProviderArgumentsProvider.GetArguments<TestArguments>();
			Assert.AreEqual( 1, arguments.LeagueId );
		}


	}

	public class TestPropertyValueProvider : IPropertyValueProvider
	{

		private readonly Dictionary<string, string> values = new Dictionary<string, string>
		{
			{ "LeagueId", "1" }
		};

		public string GetKey( Type objectType, string propertyName )
		{
			return propertyName;
		}

		public bool HasValue( string key )
		{
			return values.ContainsKey( key );
		}

		public object GetValue( Type propertyType, string key )
		{
			return Convert.ChangeType( values[ key ], propertyType );
		}
	}

	public class TestArguments
	{
		public int LeagueId { get; set; }
		public string Filter { get; set; }
	}
}
