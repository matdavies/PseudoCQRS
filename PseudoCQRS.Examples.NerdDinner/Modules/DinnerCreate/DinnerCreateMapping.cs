using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerCreate
{
	public class DinnerCreateMapping : Profile
	{
		protected override void Configure()
		{
			CreateMap<DinnerCreateViewModel, DinnerCreateCommand>();
		}
	}
}