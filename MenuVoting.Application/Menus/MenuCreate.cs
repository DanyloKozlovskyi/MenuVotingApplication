using MenuVoting.Domain;

namespace MenuVoting.Application.Menus;
public class MenuCreate
{
	public ICollection<string>? Dishes { get; set; }
	public Guid MenuPoolId { get; set; }
}