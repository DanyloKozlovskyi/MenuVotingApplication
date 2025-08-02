using MenuVoting.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuVoting.Domain.Entities;
public class Vote : IKeyedEntity<Guid>
{
	public Guid Id { get; set; }

	[ForeignKey(nameof(ApplicationUser))]
	public Guid UserId { get; set; }
	virtual public ApplicationUser? User { get; set; }
	[ForeignKey(nameof(Entities.Menu))]
	public Guid MenuId { get; set; }
	virtual public Menu? Menu { get; set; }
}
