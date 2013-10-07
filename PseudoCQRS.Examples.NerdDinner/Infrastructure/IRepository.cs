using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using PseudoCQRS.Examples.NerdDinner.Entities;

namespace PseudoCQRS.Examples.NerdDinner.Infrastructure
{
	public interface IRepository
	{
		T Get<T>( int id ) where T : BaseEntity;
		T GetSingleOrDefault<T>( Func<T, bool> predicate ) where T : BaseEntity;
		IEnumerable<T> GetAll<T>() where T : BaseEntity;
		int Save<T>( T obj ) where T : BaseEntity;
	}
}
