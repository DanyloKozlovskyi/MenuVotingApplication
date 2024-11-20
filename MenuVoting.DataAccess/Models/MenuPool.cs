using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Models
{
	public class MenuPool
	{
		public Guid Id { get; set; }
		[ForeignKey(nameof(Restaurant))]
		public Guid RestaurantId { get; set; }
		virtual public Restaurant? Restaurant { get; set; }
		virtual public List<Menu>? Menus { get; set; }
	}
}
