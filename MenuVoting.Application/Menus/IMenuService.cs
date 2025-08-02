namespace MenuVoting.Application.Menus;
public interface IMenuService
{
	Task<MenuResponse> CreateMenu(MenuCreate menuCreate);
	Task<bool> DeleteMenu(Guid id);
}
