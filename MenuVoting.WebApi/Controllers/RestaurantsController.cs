using MenuVoting.Application.Restaurants;
using MenuVoting.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuVoting.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class RestaurantsController : ControllerBase
{
	private readonly IRestaurantsService _restaurantsService;

	public RestaurantsController(IRestaurantsService service)
	{
		_restaurantsService = service;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
	{
		var restaurants = await _restaurantsService.GetRestaurants();
		return Ok(restaurants);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<Restaurant>> GetRestaurant(Guid id)
	{
		var restaurant = await _restaurantsService.GetRestaurantById(id);

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

		bool result = await _restaurantsService.UpdateRestaurant(id, restaurant);

		return Ok(result);
	}

	[HttpPost]
	public async Task<ActionResult<Restaurant>> PostRestaurant(RestaurantCreate restaurantCreate)
	{
		var restaurant = await _restaurantsService.CreateRestaurant(restaurantCreate);

		return CreatedAtAction("GetRestaurant", new { id = restaurant.Id });
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteRestaurant(Guid id)
	{
		bool deleteSucceeded = await _restaurantsService.DeleteRestaurant(id);
		if (!deleteSucceeded)
		{
			return NotFound();
		}

		return Ok(deleteSucceeded);
	}
}
