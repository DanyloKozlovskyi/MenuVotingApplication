using AutoMapper;
using MenuVoting.DataAccess;
using MenuVoting.DataAccess.Dtos;
using MenuVoting.DataAccess.Models;
using MenuVoting.WebApi.Util;
using Microsoft.EntityFrameworkCore;

namespace MenuVoting.WebApi.Services
{
	public class RestaurantsService : IRestaurantsService
	{
		private readonly MenuVotingDbContext dbContext;
		private readonly IMapper mapper;

		public RestaurantsService(MenuVotingDbContext context)
		{
			dbContext = context;

			var map = new MapperConfiguration
			(
				mc => mc.AddProfile(new MappingProfile())
			);
			mapper = map.CreateMapper();
		}

		public async Task<Restaurant> CreateRestaurant(RestaurantCreate restaurantCreate)
		{
			Restaurant restaurant = mapper.Map<Restaurant>(restaurantCreate);
			dbContext.Restaurants.Add(restaurant);
			await dbContext.SaveChangesAsync();
			return restaurant;
		}

		public async Task<bool> DeleteRestaurant(Guid id)
		{
			var restaurant = await dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
			if (restaurant == null)
			{
				return false;
			}

			dbContext.Restaurants.Remove(restaurant);
			await dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<Restaurant>> GetRestaurants()
		{
			return await dbContext.Restaurants.ToListAsync();
		}

		public async Task<Restaurant?> GetRestaurantById(Guid id)
		{
			var restaurant = await dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);

			return restaurant;
		}

		public async Task<bool> UpdateRestaurant(Guid id, Restaurant restaurant)
		{
			if (!RestaurantExists(id))
			{
				return false;
			}
			Restaurant? restaurantToUpdate = await dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == id);

			restaurantToUpdate.Name = restaurant.Name;
			return true;
		}

		private bool RestaurantExists(Guid id)
		{
			return dbContext.Restaurants.Any(e => e.Id == id);
		}
	}
}
