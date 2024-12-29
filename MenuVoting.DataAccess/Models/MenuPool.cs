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
        [ForeignKey(nameof(MenuVoting.DataAccess.Models.Restaurant))]
        public Guid RestaurantId { get; set; }
        virtual public Restaurant? Restaurant { get; set; }
        public ICollection<Menu>? Menus { get; set; }
        public DateOnly Date { get; set; }
    }
}
