using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PseudoCQRS.Examples.NerdDinner.Entities;

namespace PseudoCQRS.Examples.NerdDinner.Infrastructure
{
	public class Repository : IRepository
	{
		private static readonly Dictionary<Type, List<BaseEntity>> InternalStorageDictionary;

		static Repository()
		{
			InternalStorageDictionary = new Dictionary<Type, List<BaseEntity>>();
		}


		public T Get<T>( int id ) where T : BaseEntity
		{
			T result = default( T );
			if ( InternalStorageDictionary.ContainsKey( typeof( T ) ) )
				result = (T)InternalStorageDictionary[ typeof( T ) ].SingleOrDefault( x => x.Id == id );

			return result;
		}

		public IEnumerable<T> GetAll<T>() where T : BaseEntity
		{
			var list = new List<T>();
			if ( InternalStorageDictionary.ContainsKey( typeof ( T ) ) )
				list = InternalStorageDictionary[ typeof ( T ) ].Select( x => (T) x ).ToList();

			return list;
		}

		public int Save<T>( T obj ) where T : BaseEntity
		{
			var list = new List<BaseEntity>();
			if ( InternalStorageDictionary.ContainsKey( typeof( T ) ) )
				list = InternalStorageDictionary[ typeof( T ) ];


			if ( obj.Id == 0 )
			{

				int nextId = list.Any() ? list.Max( x => x.Id ) + 1 : 1;
				obj.Id = nextId;
			}
			else
				list.RemoveAll( x => x.Id == obj.Id );

			list.Add( obj );
			return obj.Id;
		}
	}
}