using MenuVoting.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Dtos
{
    public class MenuPoolCreate
    {
        public Guid RestaurantId { get; set; }
    }
}
