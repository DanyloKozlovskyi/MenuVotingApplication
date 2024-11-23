using MenuVoting.DataAccess.Dtos;
using MenuVoting.DataAccess.Models;
using MenuVoting.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuVoting.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class RestaurantsController : ControllerBase
	{
		private readonly IRestaurantsService restaurantsService;

		public RestaurantsController(IRestaurantsService service)
		{
			restaurantsService = service;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
		{
			var restaurant = await restaurantsService.GetRestaurants();
			return Ok(restaurant);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Restaurant>> GetRestaurant(Guid id)
		{
			var restaurant = await restaurantsService.GetRestaurantById(id);

			if (restaurant == null)
			{
				return NotFound();
			}

			return Ok(restaurant);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutRestaurant(Guid id, Restaurant restaurant)
		{
			if (id != restaurant.Id)
			{
				return BadRequest();
			}

			bool result = await restaurantsService.UpdateRestaurant(id, restaurant);

			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<Restaurant>> PostRestaurant(RestaurantCreate restaurantCreate)
		{
			var restaurant = await restaurantsService.CreateRestaurant(restaurantCreate);

			return CreatedAtAction("GetRestaurant", new { id = restaurant.Id });
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteRestaurant(Guid id)
		{
			bool deleteSucceeded = await restaurantsService.DeleteRestaurant(id);
			if (!deleteSucceeded)
			{
				return NotFound();
			}

			return Ok(deleteSucceeded);
		}
	}
}
