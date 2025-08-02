using System.ComponentModel.DataAnnotations.Schema;

namespace MenuVoting.Domain.Entities;
public class MenuPool : IKeyedEntity<Guid>
{
	public Guid Id { get; set; }
	[ForeignKey(nameof(Entities.Restaurant))]
	public Guid RestaurantId { get; set; }
	virtual public Restaurant? Restaurant { get; set; }
	public ICollection<Menu>? Menus { get; set; }
	public DateOnly Date { get; set; }
}
