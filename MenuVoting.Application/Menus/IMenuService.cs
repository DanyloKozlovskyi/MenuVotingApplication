using MenuVoting.Domain.Entities;

namespace MenuVoting.Application.Menus;
public interface IMenuService
{
	Task<Menu> CreateMenu(MenuCreate menuCreate);
	Task<bool> DeleteMenu(Guid id);
}
