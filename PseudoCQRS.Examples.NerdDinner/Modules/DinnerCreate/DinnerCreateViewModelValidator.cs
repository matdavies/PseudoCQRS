using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerCreate
{
	public class DinnerCreateViewModelValidator : AbstractValidator<DinnerCreateViewModel>
	{
		public DinnerCreateViewModelValidator()
		{
			RuleFor( x => x.Title ).NotEmpty();
			RuleFor( x => x.Description ).NotEmpty();
			RuleFor( x => x.EventDate ).NotEmpty();
		}
	}
}