using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCQRS.Examples.NerdDinner.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; protected set; }
		public string Username { get; protected set; }
		public string Password { get; protected set; }
		public bool IsAdministrator { get; protected set; }

		public User() {}

		public User( string name, string username, string password, bool isAdministrator )
		{
			this.Name = name;
			this.Username = username;
			this.Password = password;
			this.IsAdministrator = isAdministrator;
		}
	}
}