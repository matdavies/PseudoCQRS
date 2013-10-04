using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Examples.NerdDinner.Infrastructure
{
	public class NerdDinnerMappingEngine : ISpawtzMappingEngine
	{
		public TTo Map<TFrom, TTo>( TFrom viewModel )
		{
			return Mapper.Map<TFrom, TTo>( viewModel );
		}
	}
}