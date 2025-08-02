using MenuVoting.Application.Votes;
using MenuVoting.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MenuVoting.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VotesController : ControllerBase
{
	private readonly IVoteService _voteService;

	public VotesController(IVoteService service)
	{
		_voteService = service;
	}

	[HttpPost]
	public async Task<ActionResult<MenuPool>> CastVote([FromQuery] Guid menuPoolId, VoteCreate voteCreate)
	{
		Vote vote = await _voteService.CreateVote(menuPoolId, voteCreate);

		return CreatedAtAction(nameof(MenuPoolsController.GetMenuPool), new { id = menuPoolId }, voteCreate);
	}

	[HttpGet]
	public async Task<ActionResult<MenuPool>> GetCurrentVote([FromQuery] Guid menuPoolId)
	{
		var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

		var vote = await _voteService.CurrentVote(menuPoolId, userId);

		return Ok(vote);
	}
}
