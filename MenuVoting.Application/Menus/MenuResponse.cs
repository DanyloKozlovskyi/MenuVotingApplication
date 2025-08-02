using MenuVoting.Application.Votes;
using MenuVoting.Domain;
using MenuVoting.Domain.Entities;

namespace MenuVoting.Application.Menus;
public class MenuResponse : IDto<Menu, Guid>
{
	public Guid Id { get; set; }
	public ICollection<string>? Dishes { get; set; }
	public Guid MenuPoolId { get; set; }
	public ICollection<VoteCreate>? Votes { get; set; }
}
