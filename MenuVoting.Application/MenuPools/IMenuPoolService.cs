using MenuVoting.Domain.Entities;

namespace MenuVoting.Application.MenuPools;
public interface IMenuPoolService
{
	Task<IEnumerable<MenuPool>> GetMenuPools();
	Task<MenuPool?> GetMenuPoolById(Guid id);
	Task<bool> UpdateMenuPool(Guid id, MenuPool menuPool);
	Task<MenuPool> CreateMenuPool(MenuPoolCreate menuPoolCreate);
	Task<bool> DeleteMenuPool(Guid id);
	Task<MenuPool?> CurrentMenuPool(Guid restaurantId);
}
