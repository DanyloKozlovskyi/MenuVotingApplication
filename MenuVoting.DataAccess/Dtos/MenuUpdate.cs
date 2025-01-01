using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuVoting.DataAccess.Dtos
{
    public class MenuUpdate
    {
        public ICollection<string>? Dishes { get; set; }
    }
}
