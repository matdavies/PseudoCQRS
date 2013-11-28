using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoCQRS.Attributes;
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

        [Test]
        public void Persist_WhenCalled_CallCookieSetValueCorrectlyForAllPropertysWithCookieAttribute()
        {
            const string cookie1Value = "Cookie 1 Value";
            const string cookie2Value = "Cookie 2 Value";

            var testClass = new TestClass
            {
                Cookie1 = cookie1Value,
                Cookie2 = cookie2Value
            };

            var cookieValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();
            var sessionValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();

            ArrangeAct_Persist( cookieValueProvider, sessionValueProvider, testClass );

            cookieValueProvider.AssertWasCalled( x => x.SetValue<TestClass>( "Cookie1", cookie1Value ) );
            cookieValueProvider.AssertWasCalled( x => x.SetValue<TestClass>( "Cookie2", cookie2Value ) );
        }

        [Test]
        public void Persist_WhenCalled_CallCookieSetValueCorrectlyForAllPropertysWithSessionAttribute()
        {
            const string session1Value = "Session 1 Value";
            const string session2Value = "Session 2 Value";

            var testClass = new TestClass
            {
                Session1 = session1Value,
                Session2 = session2Value
            };

            var cookieValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();
            var sessionValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();

            ArrangeAct_Persist( cookieValueProvider, sessionValueProvider, testClass );

            sessionValueProvider.AssertWasCalled( x => x.SetValue<TestClass>( "Session1", session1Value ) );
            sessionValueProvider.AssertWasCalled( x => x.SetValue<TestClass>( "Session2", session2Value ) );
        }

        [Test]
        public void Persist_WhenCalled_SmokeTest()
        {
            const string cookie1Value = "Cookie 1 Value";
            const string cookie2Value = "Cookie 2 Value";
            const string session1Value = "Session 1 Value";
            const string session2Value = "Session 2 Value";

            var propertyValueProvider = MockRepository.GenerateMock<IPropertyValueProvider>();
            propertyValueProvider.Stub( x => x.HasValue<TestClass>( Arg<string>.Is.Anything ) ).Return( true );
            MockPropertyValueProvider( propertyValueProvider, "Cookie1", cookie1Value );
            MockPropertyValueProvider( propertyValueProvider, "Cookie2", cookie2Value );
            MockPropertyValueProvider( propertyValueProvider, "Session1", session1Value );
            MockPropertyValueProvider( propertyValueProvider, "Session2", session2Value );


            var cookieValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();
            var sessionValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();

            _propertyValueProviderFactory = MockRepository.GenerateMock<IPropertyValueProviderFactory>();
            _propertyValueProviderFactory.Stub( x => x.GetPropertyValueProviders() ).Return( new List<IPropertyValueProvider>
            {
                propertyValueProvider
            } );

            _propertyValueProviderFactory.Stub( x => x.GetPersistablePropertyValueProvider( PersistanceLocation.Cookie ) ).Return( cookieValueProvider );
            _propertyValueProviderFactory.Stub( x => x.GetPersistablePropertyValueProvider( PersistanceLocation.Session ) ).Return( sessionValueProvider );

            var viewModelProviderArgumentsProvider = new ViewModelProviderArgumentsProvider( _propertyValueProviderFactory );
            viewModelProviderArgumentsProvider.Persist<TestClass>();

            cookieValueProvider.AssertWasCalled( x => x.SetValue<TestClass>( "Cookie1", cookie1Value ) );
            cookieValueProvider.AssertWasCalled( x => x.SetValue<TestClass>( "Cookie2", cookie2Value ) );
            sessionValueProvider.AssertWasCalled( x => x.SetValue<TestClass>( "Session1", session1Value ) );
            sessionValueProvider.AssertWasCalled( x => x.SetValue<TestClass>( "Session2", session2Value ) );
        }

        private void ArrangeAct_Persist( IPersistablePropertyValueProvider cookieValueProvider, IPersistablePropertyValueProvider sessionValueProvider, TestClass testClass )
        {
            _propertyValueProviderFactory = MockRepository.GenerateMock<IPropertyValueProviderFactory>();
            _propertyValueProviderFactory.Stub( x => x.GetPersistablePropertyValueProvider( PersistanceLocation.Cookie ) ).Return( cookieValueProvider );
            _propertyValueProviderFactory.Stub( x => x.GetPersistablePropertyValueProvider( PersistanceLocation.Session ) ).Return( sessionValueProvider );

            var viewModelProviderArgumentsProvider = new ViewModelProviderArgumentsProvider( _propertyValueProviderFactory );
            viewModelProviderArgumentsProvider.Persist( testClass );
        }

        private static void MockPropertyValueProvider( IPropertyValueProvider propertyValueProvider, string key, string returnValue )
        {
            propertyValueProvider.Stub( x => x.GetValue<TestClass>( key, typeof ( string ) ) ).Return( returnValue );
        }

        private class TestClass
        {
            [Persist( PersistanceLocation.Cookie )]
            public String Cookie1 { get; set; }

            [Persist( PersistanceLocation.Cookie )]
            public String Cookie2 { get; set; }

            [Persist( PersistanceLocation.Session )]
            public String Session1 { get; set; }

            [Persist( PersistanceLocation.Session )]
            public String Session2 { get; set; }
        }


        //private void Persist_ArrangeAndAct( IPersistablePropertyValueProvider persistablePropertyValueProvider = null )
        //{
        //    _persistablePropertyKeyValueProvider = persistablePropertyValueProvider ?? MockRepository.GenerateMock<IPersistablePropertyValueProvider>();

        //    _propertyValueProviderFactory = StubGetPropertyValueProviders();
        //    _propertyValueProviderFactory
        //        .Stub( x => x.GetPersistablePropertyValueProvider( Arg<PersistanceLocation>.Is.Anything ) )
        //        .Return( _persistablePropertyKeyValueProvider );

        //    var argumentProvider = new ViewModelProviderArgumentsProvider( _propertyValueProviderFactory );
        //    argumentProvider.Persist<TestArguments>( x => x.LeagueId, PersistanceLocation.Session );
        //    argumentProvider.Persist<TestArguments>(x => x.Filter, PersistanceLocation.Session);
        //}

        //[Test]
        //public void Persist_CallCorrectly_GetPersistablePropertyValueProvider()
        //{
        //    Persist_ArrangeAndAct();
        //    _propertyValueProviderFactory
        //        .AssertWasCalled( x => x.GetPersistablePropertyValueProvider( PersistanceLocation.Session ) );
        //}

        //[Test]
        //public void Persist_PersistablePropertyValueProvider_CallsSetKeyAndSaveValue()
        //{
        //    const string key = "LeagueId";
        //    _persistablePropertyKeyValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();

        //    Persist_ArrangeAndAct( _persistablePropertyKeyValueProvider );
        //    _persistablePropertyKeyValueProvider.AssertWasCalled( x => x.SetValue<TestArguments>( key, 1 ) );
        //}

        //[Test]
        //public void Persist_PersistablePropertyValueProvider_CallsSetKeyForStringPropertyAndSaveValue()
        //{
        //    const string key = "Filter";
        //    _persistablePropertyKeyValueProvider = MockRepository.GenerateMock<IPersistablePropertyValueProvider>();

        //    Persist_ArrangeAndAct( _persistablePropertyKeyValueProvider );
        //    _persistablePropertyKeyValueProvider.AssertWasCalled( x => x.SetValue<TestArguments>( key, "TestVal" ) );
        //}
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

        public object GetValue<T>( string key, Type propertyType )
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