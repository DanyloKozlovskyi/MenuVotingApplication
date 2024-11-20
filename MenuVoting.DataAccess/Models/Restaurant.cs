using MenuVoting.DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Models
{
	public class Restaurant
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		virtual public IList<ApplicationUser>? Users { get; set; }
		virtual public IList<MenuPool>? MenuPools { get; set; }
	}
}
