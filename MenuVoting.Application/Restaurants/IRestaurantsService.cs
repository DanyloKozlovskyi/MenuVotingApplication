using MenuVoting.Domain.Entities;

namespace MenuVoting.Application.Restaurants;
public interface IRestaurantsService
{
	Task<IEnumerable<Restaurant>> GetRestaurants();

	Task<Restaurant?> GetRestaurantById(Guid id);

	Task<bool> UpdateRestaurant(Guid id, Restaurant restaurant);

	Task<Restaurant> CreateRestaurant(RestaurantCreate restaurantCreate);

	Task<bool> DeleteRestaurant(Guid id);
}
