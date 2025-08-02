using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenuVoting.Domain.Entities.Identity;
public class ApplicationUser : IdentityUser<Guid>
{
	public string? PersonName { get; set; }
	public string? RefreshToken { get; set; }
	public DateTime RefreshTokenExpirationDateTime { get; set; }
	[ForeignKey(nameof(Restaurant))]
	public Guid RestaurantId { get; set; }
	public Restaurant? Restaurant { get; set; }
	public virtual ICollection<Vote>? Votes { get; set; }
	public bool IsAdmin { get; set; }
}
