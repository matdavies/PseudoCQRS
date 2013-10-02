using System;
using NUnit.Framework;
using PseudoCQRS.Helpers;

namespace PseudoCQRS.Tests.Helpers
{
	[TestFixture]
	public class MemoryCacheTests
	{
		private MemoryCache _cache;

		[SetUp]
		public void Setup()
		{
			_cache = new MemoryCache();
		}

		[Test]
		public void CanReadAndWrite()
		{
			const string key = "Test124";
			const int defaultValue = 1223;
			const int savedValue = 101;
			Assert.AreEqual( defaultValue, _cache.GetValue( key, defaultValue ) );
			_cache.SetValue( key, savedValue );
			Assert.AreEqual( savedValue, _cache.GetValue( key, defaultValue ) );
		}
	}
}
