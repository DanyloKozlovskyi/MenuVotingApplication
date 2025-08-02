using AutoMapper;
using MenuVoting.Application.Mapper;
using MenuVoting.Application.Menus;
using MenuVoting.Application.Votes;
using MenuVoting.Domain;
using MenuVoting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuVoting.Application.MenuPools;
public class MenuPoolService : IMenuPoolService
{
	private readonly IEntityRepository<Guid, MenuPool> _menuPoolRepository;
	private readonly IMapper mapper;

	public MenuPoolService(IEntityRepository<Guid, MenuPool> menuPoolRepository)
	{
		_menuPoolRepository = menuPoolRepository;

		var map = new MapperConfiguration
		(
			mc => mc.AddProfile(new MappingProfile())
		);
		mapper = map.CreateMapper();
	}

	public async Task<IEnumerable<MenuPool>> GetMenuPools()
	{
		return await _menuPoolRepository.GetAll();
	}

	public async Task<MenuPool?> GetMenuPoolById(Guid id)
	{
		var menuPool = await _menuPoolRepository.GetById(id);

		return menuPool;
	}

	public async Task<bool> UpdateMenuPool(Guid id, MenuPool menuPool)
	{
		if (!await MenuPoolExists(id))
		{
			return false;
		}
		MenuPool? menuPoolToUpdate = await _menuPoolRepository.GetByIdWithDetails(id, includeProperties: nameof(MenuPool.Menus));

		menuPoolToUpdate.Menus = menuPool.Menus;

		await _menuPoolRepository.SaveChangesAsync();

		return true;
	}

	public async Task<MenuPool> CreateMenuPool(MenuPoolCreate menuPoolCreate)
	{
		var menuPool = mapper.Map<MenuPool>(menuPoolCreate);

		await _menuPoolRepository.Create(menuPool);
		await _menuPoolRepository.SaveChangesAsync();

		return menuPool;
	}

	public async Task<bool> DeleteMenuPool(Guid id)
	{
		var menuPool = await _menuPoolRepository.GetById(id);
		if (menuPool == null)
		{
			return false;
		}

		await _menuPoolRepository.Delete(menuPool);
		await _menuPoolRepository.SaveChangesAsync();

		return true;
	}
	public async Task<MenuPoolResponse?> CurrentMenuPool(Guid restaurantId)
	{
		var today = DateOnly.FromDateTime(DateTime.UtcNow);
		//string includeProperties = nameof(MenuPool.Menus);

		var menuPool = await _menuPoolRepository
			.Get(
				whereExpression: mp => mp.RestaurantId == restaurantId && mp.Date == today
			//includeProperties: includeProperties
			)
			.Select(mp => new MenuPoolResponse
			{
				Id = mp.Id,
				Date = mp.Date,
				Menus = mp.Menus
				.Select(m => new MenuResponse
				{
					Id = m.Id,
					Dishes = m.Dishes,
					MenuPoolId = m.MenuPoolId,
					Votes = m.Votes.Select(v => new VoteResponse { Id = v.Id, UserId = v.UserId })

				})
				.ToList()
			})
			.FirstOrDefaultAsync();

		return menuPool;
	}

	private Task<bool> MenuPoolExists(Guid id)
	{
		return _menuPoolRepository.Any(e => e.Id == id);
	}
}
