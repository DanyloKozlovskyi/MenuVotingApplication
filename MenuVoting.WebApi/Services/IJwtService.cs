using MenuVoting.DataAccess.Dtos;
using MenuVoting.DataAccess.Identity;
using System.Security.Claims;

namespace MenuVoting.WebApi.Services
{
	public interface IJwtService
	{
		AuthenticationResponse CreateJwtToken(ApplicationUser user);
		ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
	}
}
