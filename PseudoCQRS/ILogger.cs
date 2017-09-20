using System;

namespace PseudoCQRS
{
	public interface ILogger
	{
		void Log( string message, Exception ex, object data );
	}
}
