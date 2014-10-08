using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PseudoCQRS.ExtensionMethods
{
	internal static class DictionaryExtentionMethods
	{
		public static void AddRange<T1, T2>( this Dictionary<T1, T2> instance, Dictionary<T1, T2> dictionary )
		{
			foreach ( var item in dictionary )
				instance.Add( item.Key, item.Value );
		}
	}
}