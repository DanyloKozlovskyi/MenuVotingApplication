using MenuVoting.Domain.Entities;

namespace MenuVoting.Application.Votes;
public interface IVoteService
{
	Task<Vote> CreateVote(Guid menuPoolId, VoteCreate voteCreate);
	Task<bool> CheckExistenceOfVote(Guid menuPoolId, VoteCreate voteCreate);
	Task<Vote?> CurrentVote(Guid menuPoolId, Guid userId);
}