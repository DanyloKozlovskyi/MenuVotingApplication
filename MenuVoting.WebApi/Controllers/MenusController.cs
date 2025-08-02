using MenuVoting.Application.Menus;
using MenuVoting.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuVoting.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MenusController : ControllerBase
{
	private readonly IMenuService _menuService;

	public MenusController(IMenuService menuService)
	{
		_menuService = menuService;
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteMenu(Guid id)
	{
		bool deleteSucceeded = await _menuService.DeleteMenu(id);

		return Ok(deleteSucceeded);
	}

	[HttpPost("{id}/")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<MenuPool>> AddMenuToMenuPool(Guid id, MenuCreate menuCreate)
	{
		Menu menu = await _menuService.CreateMenu(menuCreate);

		return CreatedAtAction(nameof(MenuPoolsController.GetMenuPool), new { id }, menu);
	}
}
