using AutoMapper;
using MenuVoting.DataAccess;
using MenuVoting.DataAccess.Dtos;
using MenuVoting.DataAccess.Models;
using MenuVoting.WebApi.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuVoting.WebApi.Services
{
    public class MenuVotingsService : IMenuVotingsService
    {
        private readonly MenuVotingDbContext dbContext;
        private readonly IMapper mapper;

        public MenuVotingsService(MenuVotingDbContext context)
        {
            dbContext = context;

            var map = new MapperConfiguration
            (
                mc => mc.AddProfile(new MappingProfile())
            );
            mapper = map.CreateMapper();
        }

        public async Task<IEnumerable<MenuPool>> GetMenuPools()
        {
            return await dbContext.MenuPools.ToListAsync();
        }

        public async Task<MenuPool?> GetMenuPoolById(Guid id)
        {
            var menuPool = await dbContext.MenuPools.FirstOrDefaultAsync(x => x.Id == id);

            return menuPool;
        }

        public async Task<bool> UpdateMenuPool(Guid id, MenuPool menuPool)
        {
            if (!MenuPoolExists(id))
            {
                return false;
            }
            MenuPool? menuPoolToUpdate = await dbContext.MenuPools.FirstOrDefaultAsync(x => x.Id == id);

            menuPoolToUpdate.RestaurantId = menuPool.RestaurantId;
            return true;
        }

        public async Task<MenuPool> CreateMenuPool(MenuPoolCreate menuPoolCreate)
        {
            var menuPool = mapper.Map<MenuPool>(menuPoolCreate);

            dbContext.MenuPools.Add(menuPool);
            await dbContext.SaveChangesAsync();

            return menuPool;
        }

        public async Task<bool> DeleteMenuPool(Guid id)
        {
            var menuPool = await dbContext.MenuPools.FirstOrDefaultAsync(x => x.Id == id);
            if (menuPool == null)
            {
                return false;
            }

            dbContext.MenuPools.Remove(menuPool);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Vote> CreateVote(VoteCreate voteCreate)
        {
            if (await CheckExistenceOfVote(voteCreate))
                throw new ArgumentException(nameof(voteCreate));

            Vote vote = mapper.Map<Vote>(voteCreate);
            dbContext.Votes.Add(vote);
            await dbContext.SaveChangesAsync();

            return vote;
        }

        public async Task<bool> CheckExistenceOfVote(VoteCreate voteCreate)
        {
            var existingVote = await dbContext.Votes.FirstOrDefaultAsync(x => x.UserId == voteCreate.UserId && x.MenuId == x.MenuId);

            return existingVote == null;
        }

        public async Task<Menu> CreateMenu(MenuCreate menuCreate)
        {
            Menu menu = mapper.Map<Menu>(menuCreate);
            dbContext.Menus.Add(menu);
            await dbContext.SaveChangesAsync();

            return menu;
        }

        public async Task<bool> DeleteMenu(Guid id)
        {
            var menu = await dbContext.Menus.FirstOrDefaultAsync(x => x.Id == id);
            if (menu == null)
            {
                return false;
            }

            dbContext.Menus.Remove(menu);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<MenuPool?> CurrentMenuPool(Guid restaurantId)
        {
            return await dbContext.MenuPools.Include(x => x.Menus).FirstOrDefaultAsync(x => x.RestaurantId == restaurantId && x.Date == DateOnly.FromDateTime(DateTime.UtcNow));
        }

        private bool MenuPoolExists(Guid id)
        {
            return dbContext.MenuPools.Any(e => e.Id == id);
        }
    }
}
