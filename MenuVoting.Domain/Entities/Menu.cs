using System.ComponentModel.DataAnnotations.Schema;

namespace MenuVoting.Domain.Entities;
public class Menu : IKeyedEntity<Guid>
{
	public Guid Id { get; set; }
	public ICollection<string>? Dishes { get; set; }
	[ForeignKey(nameof(Entities.MenuPool))]
	public Guid MenuPoolId { get; set; }
	public MenuPool? MenuPool { get; set; }
	virtual public ICollection<Vote>? Votes { get; set; }
}
