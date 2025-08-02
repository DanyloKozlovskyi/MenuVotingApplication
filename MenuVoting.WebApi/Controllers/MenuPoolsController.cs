using MenuVoting.Application.MenuPools;
using MenuVoting.Application.Menus;
using MenuVoting.Application.Votes;
using MenuVoting.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuVoting.WebApi.Controllers;
[Route("api/menu-pools")]
[ApiController]
public class MenuPoolsController : ControllerBase
{
	private readonly IMenuPoolService _menuPoolService;

	public MenuPoolsController(IMenuPoolService menuPoolService)
	{
		_menuPoolService = menuPoolService;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<MenuPool>> GetMenuPool(Guid id)
	{
		var menuPool = await _menuPoolService.GetMenuPoolById(id);

		return Ok(menuPool);
	}

	[HttpGet("current")]
	public async Task<ActionResult<MenuPool>> GetCurrentMenuPool()
	{
		var restaurantId = Guid.Parse(User.FindFirstValue("Organization"));

		var menuPool = await _menuPoolService.CurrentMenuPool(restaurantId);

		return Ok(menuPool);
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> PutMenuPool(Guid id, MenuPool menuPool)
	{
		bool result = await _menuPoolService.UpdateMenuPool(id, menuPool);

		return Ok(result);
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<MenuPool>> PostMenuPool(MenuPoolCreate menuPoolCreate)
	{
		var menuPool = await _menuPoolService.CreateMenuPool(menuPoolCreate);

		return CreatedAtAction(nameof(GetMenuPool), new { id = menuPool.Id }, menuPool);
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteMenuPool(Guid id)
	{
		bool deleteSucceeded = await _menuPoolService.DeleteMenuPool(id);

		return Ok(deleteSucceeded);
	}
}
