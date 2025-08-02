using MenuVoting.Application.MenuPools;
using MenuVoting.Application.Menus;
using MenuVoting.Application.MenuVotings;
using MenuVoting.Application.Votes;
using MenuVoting.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuVoting.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MenuVotingsController : ControllerBase
{
	private readonly IMenuVotingsService menuVotingService;

	public MenuVotingsController(IMenuVotingsService service)
	{
		menuVotingService = service;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<MenuPool>> GetMenuPool(Guid id)
	{
		var menuPool = await menuVotingService.GetMenuPoolById(id);

		if (menuPool == null)
		{
			return NotFound();
		}

		return Ok(menuPool);
	}

	[HttpGet("current")]
	public async Task<ActionResult<MenuPool>> GetCurrentMenuPool()
	{
		var restaurantId = Guid.Parse(User.FindFirstValue("Organization"));

		var menuPool = await menuVotingService.CurrentMenuPool(restaurantId);

		return Ok(menuPool);
	}

	[HttpGet("{menuPoolId}/vote/current")]
	public async Task<ActionResult<MenuPool>> GetCurrentVote(Guid menuPoolId)
	{
		var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

		var vote = await menuVotingService.CurrentVote(menuPoolId, userId);

		if (vote == null)
		{
			return Problem();
		}

		return Ok(vote);
	}

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> PutMenuPool(Guid id, MenuPool menuPool)
	{
		bool result = await menuVotingService.UpdateMenuPool(id, menuPool);

		return Ok(result);
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<MenuPool>> PostMenuPool(MenuPoolCreate menuPoolCreate)
	{
		var menuPool = await menuVotingService.CreateMenuPool(menuPoolCreate);

		return CreatedAtAction(nameof(GetMenuPool), new { id = menuPool.Id }, menuPool);
	}

	[HttpPost("{id}/menu")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<MenuPool>> AddMenuToMenuPool(Guid id, MenuCreate menuCreate)
	{
		Menu menu = await menuVotingService.CreateMenu(menuCreate);

		return CreatedAtAction(nameof(GetMenuPool), new { id }, menu);
	}

	[HttpPost("{menuPoolId}/vote")]
	public async Task<ActionResult<MenuPool>> CastVote(Guid menuPoolId, VoteCreate voteCreate)
	{
		Vote vote = await menuVotingService.CreateVote(menuPoolId, voteCreate);

		return CreatedAtAction(nameof(GetMenuPool), new { id = menuPoolId }, voteCreate);
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteMenuPool(Guid id)
	{
		bool deleteSucceeded = await menuVotingService.DeleteMenuPool(id);
		if (!deleteSucceeded)
		{
			return NotFound();
		}

		return Ok(deleteSucceeded);
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete("menu/{id}")]
	public async Task<IActionResult> DeleteMenu(Guid id)
	{
		bool deleteSucceeded = await menuVotingService.DeleteMenu(id);
		if (!deleteSucceeded)
		{
			return NotFound();
		}

		return Ok(deleteSucceeded);
	}
}
