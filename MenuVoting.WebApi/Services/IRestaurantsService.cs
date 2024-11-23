using MenuVoting.DataAccess.Dtos;
using MenuVoting.DataAccess.Models;

namespace MenuVoting.WebApi.Services
{
	public interface IRestaurantsService
	{
		Task<IEnumerable<Restaurant>> GetRestaurants();

		Task<Restaurant?> GetRestaurantById(Guid id);

		Task<bool> UpdateRestaurant(Guid id, Restaurant restaurant);

		Task<Restaurant> CreateRestaurant(RestaurantCreate restaurantCreate);

		Task<bool> DeleteRestaurant(Guid id);
	}
}
