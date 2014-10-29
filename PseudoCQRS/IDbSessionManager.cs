namespace PseudoCQRS
{
	public interface IDbSessionManager
	{
		void CloseSession();
		void OpenTransaction();
		void CommitTransaction();
		void RollbackTransaction();
	}
}