using MenuVoting.Application.Menus;
using MenuVoting.Domain;

namespace MenuVoting.Application.MenuPools;
public class MenuPoolUpdate
{
	public ICollection<MenuUpdate>? Menus { get; set; }
}
