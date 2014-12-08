namespace PseudoCQRS.Controllers
{
	public interface IMessageManager
	{
		void SetErrorMessage( string message );
		string GetErrorMessage();

		void SetSuccessMessage( string message );
		string GetSuccessMessage();
	}
}