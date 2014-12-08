using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Tests.Controllers.Helpers
{
	public class DummyCommandController : BaseCommandController<DummyCommandViewModel, DummyCommandCommand> {}
}