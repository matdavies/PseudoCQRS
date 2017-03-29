using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Tests.Controllers.Helpers
{
	public class DummyCommandController : BaseCommandController<DummyCommandViewModel, DummyCommandCommand> {
		public override ActionResult OnSuccessfulExecution( DummyCommandViewModel viewModel, CommandResult commandResult )
		{
			throw new NotImplementedException();
		}

		public override ActionResult OnFailureExecution( DummyCommandViewModel viewModel, CommandResult commandResult )
		{
			throw new NotImplementedException();
		}
	}
}