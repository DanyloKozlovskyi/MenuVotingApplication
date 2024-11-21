﻿using MenuVoting.DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Models
{
	public class Menu
	{
		public Guid Id { get; set; }
		public ICollection<string>? Components { get; set; }
		[ForeignKey(nameof(MenuVoting.DataAccess.Models.MenuPool))]
		public Guid MenuPoolId { get; set; }
		virtual public MenuPool? MenuPool { get; set; }
		virtual public ICollection<Vote>? Votes { get; set; }
	}
}
