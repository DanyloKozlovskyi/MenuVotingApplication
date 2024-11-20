using MenuVoting.DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Models
{
	public class Vote
	{
		public Guid Id { get; set; }

		[ForeignKey(nameof(ApplicationUser))]
		public Guid UserId { get; set; }
		virtual public ApplicationUser? User { get; set; }
		[ForeignKey(nameof(Menu))]
		public Guid MenuId { get; set; }
		virtual public Menu? Menu { get; set; }
	}
}
