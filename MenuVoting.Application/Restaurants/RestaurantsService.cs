using AutoMapper;
using MenuVoting.Application.Mapper;
using MenuVoting.Domain;
using MenuVoting.Domain.Entities;

namespace MenuVoting.Application.Restaurants
{
	public class RestaurantsService : IRestaurantsService
	{
		private readonly IEntityRepository<Guid, Restaurant> _repository;
		private readonly IMapper mapper;

		public RestaurantsService(IEntityRepository<Guid, Restaurant> repository)
		{
			_repository = repository;

			var map = new MapperConfiguration
			(
				mc => mc.AddProfile(new MappingProfile())
			);
			mapper = map.CreateMapper();
		}

		public async Task<Restaurant> CreateRestaurant(RestaurantCreate restaurantCreate)
		{
			Restaurant restaurant = mapper.Map<Restaurant>(restaurantCreate);
			await _repository.Create(restaurant);
			await _repository.SaveChangesAsync();
			return restaurant;
		}

		public async Task<bool> DeleteRestaurant(Guid id)
		{
			var restaurant = await _repository.GetById(id);
			if (restaurant == null)
			{
				return false;
			}

			await _repository.Delete(restaurant);
			await _repository.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<Restaurant>> GetRestaurants()
		{
			return await _repository.GetAll();
		}

		public async Task<Restaurant?> GetRestaurantById(Guid id)
		{
			var restaurant = await _repository.GetById(id);

			return restaurant;
		}

		public async Task<bool> UpdateRestaurant(Guid id, Restaurant restaurant)
		{
			if (!await RestaurantExists(id))
			{
				return false;
			}
			Restaurant? restaurantToUpdate = await _repository.GetById(id);

			restaurantToUpdate.Name = restaurant.Name;
			return true;
		}

		private Task<bool> RestaurantExists(Guid id)
		{
			return _repository.Any(e => e.Id == id);
		}
	}
}
