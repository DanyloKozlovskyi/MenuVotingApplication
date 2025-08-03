using MenuVoting.Application.Identity;
using MenuVoting.Application.MenuPools;
using MenuVoting.Application.Menus;
using MenuVoting.Application.Restaurants;
using MenuVoting.Application.Votes;
using MenuVoting.Domain;
using MenuVoting.Domain.Entities.Identity;
using MenuVoting.Persistence;
using MenuVoting.Persistence.Configurations.SeedRoles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
	var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddControllers();
builder.Services.AddDbContext<MenuVotingDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<MenuVotingDbContext>()
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, MenuVotingDbContext, Guid>>()
.AddRoleStore<RoleStore<ApplicationRole, MenuVotingDbContext, Guid>>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateAudience = true,
		ValidAudience = builder.Configuration["Jwt:Audience"],
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
	};
});

builder.Services.AddScoped(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuPoolService, MenuPoolService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddScoped<IRestaurantsService, RestaurantsService>();
builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "MenuVoting WebApi", Version = "1.0" });
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policyBuilder =>
	{
		policyBuilder.WithOrigins("http://localhost:4200", "https://localhost:4200")
		.AllowAnyHeader()
		.AllowAnyMethod()
		.AllowCredentials();
	});
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHsts();
app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
	});
}


using (var scope = app.Services.CreateScope())
{
	try
	{
		await RoleInitializer.SeedRoles(scope.ServiceProvider);
	}
	catch (Exception exc)
	{
		Console.WriteLine(exc);
	}
}

app.Run();