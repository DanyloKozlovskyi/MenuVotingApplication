using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Dtos
{
	public class RestaurantCreate
	{
		[Required(ErrorMessage = "restaurant name can't be blank")]
		public string Name { get; set; }
		[Required(ErrorMessage = "restaurant address can't be blank")]
		public string Address { get; set; }
	}
}
