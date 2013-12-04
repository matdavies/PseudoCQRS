using System;
using System.Collections.Generic;
using NUnit.Framework;
using PseudoCQRS.PropertyValueProviders;

namespace PseudoCQRS.Tests
{
    [TestFixture]
    public class PropertyValueProviderTests
    {
        private TestPropertyValueConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new TestPropertyValueConverter();
        }

        [Test]
        public void CanConvertStringToInt()
        {
            var result = _converter.GetValue( "1", typeof ( Int32 ) );
            Assert.AreEqual( 1, result );
        }

        [Test]
        public void CanConvertCommaSeparatedListToList()
        {
            var result = _converter.GetValue( "1,2,3", typeof ( List<Int32> ) );
            Assert.AreEqual( 3, ( (List<Int32>)result ).Count );
        }

        [Test]
        public void CanConvertDateFormats( [Values( "10/11/2012", "10 Nov 2012" )] string format )
        {
            var result = (DateTime)_converter.GetValue( format, typeof ( DateTime ) );
            Assert.IsTrue( new DateTime( 2012, 11, 10 ) == result );
        }

        [Test]
        public void CanConverTimeFormats( [Values( "15:14", "03:14 pm", "3:14 pm" )] string format )
        {
            var result = (DateTime)_converter.GetValue( format, typeof ( DateTime ) );
            Assert.IsTrue( result.Hour == 15 && result.Minute == 14 && result.Second == 0 );
        }

        [Test]
        public void CanConvertDateWithTimeFormats(
            [Values( "10/11/2012 03:14 pm", "10/11/2012 3:14 pm", "10/11/2012 15:14:00" )] string format )
        {
            var result = (DateTime)_converter.GetValue( format, typeof ( DateTime ) );
            Assert.IsTrue( new DateTime( 2012, 11, 10, 15, 14, 0 ) == result );
        }

        [Test]
        public void CanConvertStringToNullableDateTime()
        {
            var result = (DateTime?)_converter.GetValue( "30/11/2013", typeof ( DateTime? ) );
            Assert.IsTrue( result.HasValue );
            Assert.AreEqual( result, new DateTime( 2013, 11, 30 ) );
        }


        [Test]
        public void ShouldReturnNullWhenTypeIsDateTimeButFormateIsNotRecognized()
        {
            Assert.IsNull( _converter.GetValue( "10-11-2012", typeof ( DateTime ) ) );
        }
    }

    internal class TestPropertyValueConverter : BasePropertyValueProvider
    {
        public object GetValue( string value, Type propertyType )
        {
            return ConvertValue( value, propertyType );
        }
    }
}