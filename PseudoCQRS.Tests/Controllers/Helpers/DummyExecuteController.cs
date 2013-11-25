using PseudoCQRS.Controllers;

namespace PseudoCQRS.Tests.Controllers.Helpers
{
    public class DummyExecuteController : BaseExecuteController<DummyExecuteViewModel, DummyExecuteCommand>
    {
        public DummyExecuteController( ICommandExecutor commandExecutor, IMessageManager messageManager, IReferrerProvider referrerProvider )
            : base( commandExecutor, messageManager, referrerProvider ) {}

        public DummyExecuteController()
        {
            
        }
    }
}