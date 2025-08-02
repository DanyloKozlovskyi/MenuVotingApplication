using MenuVoting.Application.Menus;
using MenuVoting.Domain;
using MenuVoting.Domain.Entities;

namespace MenuVoting.Application.MenuPools;
public class MenuPoolResponse : IDto<MenuPool, Guid>
{
	public Guid Id { get; set; }
	public DateOnly Date { get; set; }
	public IEnumerable<MenuResponse>? Menus { get; set; }
}
