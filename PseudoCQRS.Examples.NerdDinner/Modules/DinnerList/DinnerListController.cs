using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Examples.NerdDinner.Modules.DinnerList
{
	public class DinnerListController : BaseReadController<DinnerListViewModel, DinnerListArguments>
	{
		public override string ViewPath
		{
			get { return "Dinner/List"; }
		}
	}
}