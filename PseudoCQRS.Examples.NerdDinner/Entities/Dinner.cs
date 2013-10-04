using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCQRS.Examples.NerdDinner.Entities
{
	public class Dinner : BaseEntity
	{
		public string Title { get; protected set; }
		public DateTime EventDate { get; protected set; }
		public string Description { get; protected set; }
		public Host HostedBy { get; protected set; }

		public Dinner()
		{

		}

		public Dinner( string title, DateTime eventDate, string description, Host hostedBy )
		{
			this.Title = title;
			this.EventDate = eventDate;
			this.Description = description;
			this.HostedBy = hostedBy;
		}
	}
}