using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;
using Rhino.Mocks;

namespace PseudoCQRS.Tests
{
    public class ViewModelProviderArgumentsProviderTests
    {
        private IPropertyValueProviderFactory _propertyValueProviderFactory;
        private IPersistablePropertyValueProvider _persistablePropertyKeyValueProvider;


        private IPropertyValueProviderFactory StubGetPropertyValueProviders()
        {
            var propertyValueProviderFactory = MockRepository.GenerateMock<IPropertyValueProviderFactory>();
            var propertyValueProviders = new List<IPropertyValueProvider>
            {
                new TestPropertyValueProvider()
            };
            propertyValueProviderFactory.Stub( x => x.GetPropertyValueProviders() ).Return( propertyValueProviders );

            return propertyValueProviderFactory;
        }

        [Test]
        public void CanFillArgument()
        {
            var propertyValueProviderFactory = StubGetPropertyValueProviders();
            var viewModelProviderArgumentsProvider = new ViewModelProviderArgumentsProvider( propertyValueProviderFactory );
            var arguments = viewModelProviderArgumentsProvider.GetArguments<TestArguments>();
            Assert.AreEqual( 1, arguments.LeagueId );
        }


        private void Persist_ArrangeAndAct( IPersistablePropertyValueProvider persistablePropertyValueProvider = null )
        {
            _persistablePropertyKeyValueProvider = persistablePropertyValueProvider ?? MockRepository.GenerateMock<IPersistablePropertyValueProvider>();

            _propertyValueProviderFactory = StubGetPropertyValueProviders();
            _propertyValueProviderFactory
                .Stub( x => x.GetPersistablePropertyValueProvider( Arg<PersistanceLocation>.Is.Anything ) )
                .Return( _persistablePropertyKeyValueProvider );

            var argumentProvider = new ViewModelProviderArgumentsProvider( _propertyValueProviderFactory );
            argumentProvider.Persist<TestArguments>( x => x.LeagueId, PersistanceLocation.Session );
            argumentProvider.Persist<TestArguments>(x => x.Filter, PersistanceLocation.Session);
        }

        [Test]
        public void Persist_CallCorrectly_GetPersistablePropertyValueProvider()
        {
            Persist_ArrangeAndAct();
            _propertyValueProviderFactory
                .AssertWasCalled( x => x.GetPersistablePropertyValueProvider( PersistanceLocation.Session ) );
        }

        [Test]
        public void Persist_PersistablePropertyValueProvider_CallsSetKeyAndSaveValue()
        {
            const string key = "LeagueId";
            _persistablePropertyKeyValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();

            Persist_ArrangeAndAct( _persistablePropertyKeyValueProvider );
            _persistablePropertyKeyValueProvider.AssertWasCalled( x => x.SetValue<TestArguments>( key, 1 ) );
        }

        [Test]
        public void Persist_PersistablePropertyValueProvider_CallsSetKeyForStringPropertyAndSaveValue()
        {
            const string key = "Filter";
            _persistablePropertyKeyValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();

            Persist_ArrangeAndAct( _persistablePropertyKeyValueProvider );
            _persistablePropertyKeyValueProvider.AssertWasCalled( x => x.SetValue<TestArguments>( key, "TestVal" ) );
        }
    }

    public class TestPropertyValueProvider : IPropertyValueProvider
    {
        private readonly Dictionary<string, string> values = new Dictionary<string, string>
        {
            {
                "LeagueId", "1"
            },
            {
                "Filter", "TestVal"
            }
        };


        public bool HasValue<T>( string key )
        {
            return values.ContainsKey( key );
        }

        public object GetValue<T>(  string key, Type propertyType )
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