using AutoMapper;
using MenuVoting.DataAccess.Dtos;
using MenuVoting.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MenuVoting.WebApi.Services
{
    public interface IMenuVotingsService
    {
        Task<IEnumerable<MenuPool>> GetMenuPools();

        Task<MenuPool?> GetMenuPoolById(Guid id);

        Task<bool> UpdateMenuPool(Guid id, MenuPool menuPool);

        Task<MenuPool> CreateMenuPool(MenuPoolCreate menuPoolCreate);

        Task<bool> DeleteMenuPool(Guid id);

        Task<Vote> CreateVote(VoteCreate voteCreate);

        Task<bool> CheckExistenceOfVote(VoteCreate voteCreate);

        Task<Menu> CreateMenu(MenuCreate menuCreate);

        Task<MenuPool?> CurrentMenuPool(Guid restaurantId);

        Task<bool> DeleteMenu(Guid id);
    }
}
