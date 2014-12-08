using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCQRS.Examples.NerdDinner.Infrastructure
{
	public class NerdDinnerDbSessionManager : IDbSessionManager
	{
		public void CloseSession() {}

		public void OpenTransaction() {}

		public void CommitTransaction() {}

		public void RollbackTransaction() {}
	}
}