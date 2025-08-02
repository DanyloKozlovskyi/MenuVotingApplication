using MenuVoting.Domain.Entities.Identity;

namespace MenuVoting.Domain.Entities;
public class Restaurant : IKeyedEntity<Guid>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Address { get; set; }
	virtual public ICollection<ApplicationUser>? Users { get; set; }
	virtual public ICollection<MenuPool>? MenuPools { get; set; }
}
