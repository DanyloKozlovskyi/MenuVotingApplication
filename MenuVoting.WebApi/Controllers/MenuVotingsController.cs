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
using System.Security.Claims;
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

        [HttpGet("current")]
        public async Task<ActionResult<MenuPool>> GetCurrentMenuPool()
        {
            var restaurantId = Guid.Parse(User.FindFirstValue("Organization"));

            var menuPool = await menuVotingService.CurrentMenuPool(restaurantId);

            if (menuPool == null)
            {
                return Problem();
            }

            return Ok(menuPool);
        }

        // PUT: api/MenuPools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MenuPool>> PostMenuPool(MenuPoolCreate menuPoolCreate)
        {
            var menuPool = await menuVotingService.CreateMenuPool(menuPoolCreate);

            return CreatedAtAction("GetMenuPool", new { id = menuPool.Id });
        }

        [HttpPost("{id}/menu")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MenuPool>> AddMenuToMenuPool(Guid id, MenuCreate menuCreate)
        {
            menuCreate.MenuPoolId = id;
            Menu menu = await menuVotingService.CreateMenu(menuCreate);

            return CreatedAtAction("GetMenuPool", new { id });
        }

        [HttpPost("vote")]
        public async Task<ActionResult<MenuPool>> PostVote(VoteCreate voteCreate)
        {
            Vote vote = await menuVotingService.CreateVote(voteCreate);

            return CreatedAtAction("GetMenuPool", new { id = vote.Id });
        }

        // DELETE: api/MenuPools/5
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

        // DELETE: api/MenuPools/5
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
}
