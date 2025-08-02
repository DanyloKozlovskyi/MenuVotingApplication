using MenuVoting.Domain.Entities;

namespace MenuVoting.Application.Votes;
public interface IVoteService
{
	Task<VoteResponse> CreateVote(Guid menuPoolId, VoteCreate voteCreate);
	Task<bool> CheckExistenceOfVote(Guid menuPoolId, VoteCreate voteCreate);
	Task<VoteResponse?> CurrentVote(Guid menuPoolId, Guid userId);
}