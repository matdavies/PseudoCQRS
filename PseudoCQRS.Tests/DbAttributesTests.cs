using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Rhino.Mocks;

namespace PseudoCQRS.Tests
{
	[TestFixture]
	public class DbAttributesTests
	{
		private IDbSessionManager _dbSessionManager;
		private IServiceLocator _serviceLocator;

		[SetUp]
		public void Setup()
		{
			_dbSessionManager = MockRepository.GenerateMock<IDbSessionManager>();
			_serviceLocator = MockRepository.GenerateMock<IServiceLocator>();
			_serviceLocator
				.Stub( x => x.GetInstance<IDbSessionManager>() )
				.Return( _dbSessionManager );

			ServiceLocator.SetLocatorProvider( () => _serviceLocator );
		}


		[Test]
		public void DbTransactionAttribute_ShouldOpenTransactionOnActionExecuting()
		{
			var attribute = new DbTransactionAttribute();
			attribute.OnActionExecuting( null );
			_dbSessionManager.AssertWasCalled( x => x.OpenTransaction() );
		}

		[Test]
		public void DbTransactionAttribute_ShouldCommitTransactionOnResultExecuted()
		{
			var attribute = new DbTransactionAttribute();
			attribute.OnResultExecuted( null );
			_dbSessionManager.AssertWasCalled( x => x.CommitTransaction() );
		}

		[Test]
		public void DbSessionManagerAttribute_ShouldCloseSessionOnResultExcecuted()
		{
			var attribute = new DbSessionManagerAttribute();
			attribute.OnResultExecuted( new ResultExecutedContext() );
			_dbSessionManager.AssertWasCalled( x => x.CloseSession() );
		}
	}
}