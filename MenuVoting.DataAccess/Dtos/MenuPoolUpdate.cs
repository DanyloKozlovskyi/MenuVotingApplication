using MenuVoting.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Dtos
{
    public class MenuPoolUpdate
    {
        public ICollection<MenuUpdate>? Menus { get; set; }
    }
}
