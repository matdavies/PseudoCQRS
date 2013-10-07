using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace PseudoCQRS.Examples.NerdDinner.Modules.Login
{
	public class LoginMapping : Profile
	{
		protected override void Configure()
		{
			CreateMap<LoginViewModel, LoginCommand>();
		}
	}
}