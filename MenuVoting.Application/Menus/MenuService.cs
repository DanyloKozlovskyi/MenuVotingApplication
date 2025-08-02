using AutoMapper;
using MenuVoting.Application.Mapper;
using MenuVoting.Domain;
using MenuVoting.Domain.Entities;

namespace MenuVoting.Application.Menus;
public class MenuService : IMenuService
{
	private readonly IEntityRepository<Guid, Menu> _menuRepository;
	private readonly IMapper mapper;

	public MenuService(IEntityRepository<Guid, Menu> menuRepository)
	{
		_menuRepository = menuRepository;

		var map = new MapperConfiguration
		(
			mc => mc.AddProfile(new MappingProfile())
		);
		mapper = map.CreateMapper();
	}
	public async Task<Menu> CreateMenu(MenuCreate menuCreate)
	{
		Menu menu = mapper.Map<Menu>(menuCreate);
		await _menuRepository.Create(menu);
		await _menuRepository.SaveChangesAsync();

		return menu;
	}

	public async Task<bool> DeleteMenu(Guid id)
	{
		var menu = await _menuRepository.GetById(id);
		if (menu == null)
		{
			return false;
		}

		await _menuRepository.Delete(menu);
		await _menuRepository.SaveChangesAsync();

		return true;
	}
}