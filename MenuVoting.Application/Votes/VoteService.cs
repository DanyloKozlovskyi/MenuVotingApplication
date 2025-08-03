using AutoMapper;
using MenuVoting.Application.Mapper;
using MenuVoting.Application.Menus;
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

	public async Task<VoteResponse> CreateVote(Guid menuPoolId, VoteCreate voteCreate)
	{
		if (await CheckExistenceOfVote(menuPoolId, voteCreate))
		{
			var voteToChange = await _voteRepository.Get(whereExpression: x => x.UserId == voteCreate.UserId && x.Menu.MenuPool.Id == menuPoolId).FirstOrDefaultAsync();
			voteToChange.MenuId = voteCreate.MenuId;

			await _voteRepository.SaveChangesAsync();
			return mapper.Map<VoteResponse>(voteToChange);
		}

		Vote vote = mapper.Map<Vote>(voteCreate);
		await _voteRepository.Create(vote);
		await _voteRepository.SaveChangesAsync();

		return mapper.Map<VoteResponse>(vote);
	}

	public async Task<bool> CheckExistenceOfVote(Guid menuPoolId, VoteCreate voteCreate)
	{
		var existingVote = await CurrentVote(menuPoolId, voteCreate.UserId);

		return existingVote != null;
	}

	public async Task<VoteResponse?> CurrentVote(Guid menuPoolId, Guid userId)
	{

		var vote = await _voteRepository
			.Get(
				whereExpression: v =>
					v.UserId == userId
					&& v.Menu.MenuPool.Id == menuPoolId
			)
			.Select(v => new VoteResponse
			{
				Id = v.Id,
				MenuId = v.MenuId,
				UserId = v.UserId,
				Menu = new MenuResponse
				{
					Id = v.Menu.Id,
					MenuPoolId = v.Menu.MenuPool.Id,
					Dishes = v.Menu.Dishes,
				}
			})
			.FirstOrDefaultAsync();
		return vote;
	}
}
