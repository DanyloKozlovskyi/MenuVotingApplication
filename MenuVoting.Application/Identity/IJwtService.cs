using MenuVoting.Domain.Entities.Identity;
using System.Security.Claims;

namespace MenuVoting.Application.Identity;
public interface IJwtService
{
	AuthenticationResponse CreateJwtToken(ApplicationUser user);
	ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
}