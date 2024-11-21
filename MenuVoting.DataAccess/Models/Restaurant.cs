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
		virtual public ICollection<ApplicationUser>? Users { get; set; }
		virtual public ICollection<MenuPool>? MenuPools { get; set; }
	}
}
