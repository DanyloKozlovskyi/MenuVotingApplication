using MenuVoting.Application.Menus;
using MenuVoting.Domain;
using MenuVoting.Domain.Entities;
using MenuVoting.Domain.Entities.Identity;

namespace MenuVoting.Application.Votes;
public class VoteResponse : IDto<Vote, Guid>
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	virtual public ApplicationUser? User { get; set; }
	public Guid MenuId { get; set; }
	public MenuResponse Menu { get; set; }
}
