using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Dtos
{
	public class MenuCreate
	{
		public ICollection<string>? Components { get; set; }
		public Guid MenuPoolId { get; set; }
	}
}
