using AutoMapper;
using MenuVoting.Application.Mapper;
using MenuVoting.Domain;
using MenuVoting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MenuVoting.Application.Votes;
public class VoteService : IVoteService
{
	private readonly IEntityRepository<Guid, Vote> _voteRepository;
	private readonly IMapper mapper;

	public VoteService(IEntityRepository<Guid, Vote> voteRepository)
	{
		_voteRepository = voteRepository;

		var map = new MapperConfiguration
		(
			mc => mc.AddProfile(new MappingProfile())
		);
		mapper = map.CreateMapper();
	}

	public async Task<Vote> CreateVote(Guid menuPoolId, VoteCreate voteCreate)
	{
		if (await CheckExistenceOfVote(menuPoolId, voteCreate))
		{
			var voteToChange = await CurrentVote(menuPoolId, voteCreate.UserId);
			voteToChange.MenuId = voteCreate.MenuId;

			await _voteRepository.SaveChangesAsync();
			return voteToChange;
		}

		Vote vote = mapper.Map<Vote>(voteCreate);
		await _voteRepository.Create(vote);
		await _voteRepository.SaveChangesAsync();

		return vote;
	}

	public async Task<bool> CheckExistenceOfVote(Guid menuPoolId, VoteCreate voteCreate)
	{
		var existingVote = await CurrentVote(menuPoolId, voteCreate.UserId);

		return existingVote != null;
	}

	public async Task<Vote?> CurrentVote(Guid menuPoolId, Guid userId)
	{
		string includeProperties = $"{nameof(Vote.Menu)},{nameof(Vote.Menu)}.{nameof(Menu.MenuPool)}";

		var vote = await _voteRepository
			.Get(
				whereExpression: v =>
					v.UserId == userId
					&& v.Menu.MenuPool.Id == menuPoolId,
				includeProperties: includeProperties
			)
			.FirstOrDefaultAsync();
		return vote;
	}
}
