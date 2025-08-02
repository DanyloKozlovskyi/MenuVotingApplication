using AutoMapper;
using MenuVoting.Application.Mapper;
using MenuVoting.Application.MenuPools;
using MenuVoting.Application.Menus;
using MenuVoting.Application.Votes;
using MenuVoting.Domain;
using MenuVoting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuVoting.Application.MenuVotings;
public class MenuVotingsService : IMenuVotingsService
{
	private readonly IEntityRepository<Guid, Menu> _menuRepository;
	private readonly IEntityRepository<Guid, MenuPool> _menuPoolRepository;
	private readonly IEntityRepository<Guid, Vote> _voteRepository;

	private readonly IMapper mapper;

	public MenuVotingsService(IEntityRepository<Guid, Menu> menuRepository, IEntityRepository<Guid, MenuPool> menuPoolRepository, IEntityRepository<Guid, Vote> voteRepository)
	{

		_menuRepository = menuRepository;
		_menuPoolRepository = menuPoolRepository;
		_voteRepository = voteRepository;

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

	public async Task<Vote> CreateVote(Guid menuPoolId, VoteCreate voteCreate)
	{
		if (await CheckExistenceOfVote(menuPoolId, voteCreate))
		{
			var voteToChange = await CurrentVote(menuPoolId, voteCreate.UserId);
			voteToChange.MenuId = voteCreate.MenuId;

			await _voteRepository.SaveChangesAsync();
			return voteToChange;
		}

		Vote vote = mapper.Map<Vote>(voteCreate);
		await _voteRepository.Create(vote);
		await _voteRepository.SaveChangesAsync();

		return vote;
	}

	public async Task<bool> CheckExistenceOfVote(Guid menuPoolId, VoteCreate voteCreate)
	{
		var existingVote = await CurrentVote(menuPoolId, voteCreate.UserId);

		return existingVote != null;
	}

	public async Task<Vote?> CurrentVote(Guid menuPoolId, Guid userId)
	{
		string includeProperties = $"{nameof(Vote.Menu)},{nameof(Vote.Menu)}.{nameof(Menu.MenuPool)}";

		var vote = await _voteRepository
			.Get(
				whereExpression: v =>
					v.UserId == userId
					&& v.Menu.MenuPool.Id == menuPoolId,
				includeProperties: includeProperties
			)
			.FirstOrDefaultAsync();
		return vote;
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

	public async Task<MenuPool?> CurrentMenuPool(Guid restaurantId)
	{
		var today = DateOnly.FromDateTime(DateTime.UtcNow);
		string includeProperties = nameof(MenuPool.Menus);

		var menuPool = await _menuPoolRepository
			.Get(
				whereExpression: mp => mp.RestaurantId == restaurantId && mp.Date == today,
				includeProperties: includeProperties
			)
			.FirstOrDefaultAsync();

		return menuPool;
	}

	private Task<bool> MenuPoolExists(Guid id)
	{
		return _menuPoolRepository.Any(e => e.Id == id);
	}
}
