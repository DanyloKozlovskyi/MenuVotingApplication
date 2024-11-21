using MenuVoting.DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Dtos
{
	public class VoteCreate
	{
		public Guid UserId { get; set; }
		public Guid MenuId { get; set; }
	}
}
