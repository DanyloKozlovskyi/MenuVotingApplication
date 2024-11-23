using MenuVoting.DataAccess;
using MenuVoting.DataAccess.Dtos;
using MenuVoting.DataAccess.Models;
using MenuVoting.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuVoting.WebApi.Controllers
{
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

		// GET: api/MenuPools
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MenuPool>>> GetMenuPools()
		{
			var menuPools = await menuVotingService.GetMenuPools();
			return Ok(menuPools);
		}

		// GET: api/MenuPools/5
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

		// PUT: api/MenuPools/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutMenuPool(Guid id, MenuPool menuPool)
		{
			if (id != menuPool.Id)
			{
				return BadRequest();
			}

			bool result = await menuVotingService.UpdateMenuPool(id, menuPool);

			return Ok(result);
		}

		// POST: api/MenuPools
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<MenuPool>> PostMenuPool(MenuPool menuPool)
		{
			await menuVotingService.CreateMenuPool(menuPool);

			return CreatedAtAction("GetMenuPool", new { id = menuPool.Id });
		}

		[HttpPost("menu")]
		public async Task<ActionResult<MenuPool>> PostMenu(MenuCreate menuCreate)
		{
			Menu menu = await menuVotingService.CreateMenu(menuCreate);

			return CreatedAtAction("GetMenuPool", new { id = menu.Id });
		}

		[HttpPost("vote")]
		public async Task<ActionResult<MenuPool>> PostVote(VoteCreate voteCreate)
		{
			Vote vote = await menuVotingService.CreateVote(voteCreate);

			return CreatedAtAction("GetMenuPool", new { id = vote.Id });
		}

		// DELETE: api/MenuPools/5
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
	}
}
