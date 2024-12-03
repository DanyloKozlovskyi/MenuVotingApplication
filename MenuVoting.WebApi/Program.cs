using MenuVoting.DataAccess;
using MenuVoting.DataAccess.Identity;
using MenuVoting.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
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

// Enable identity 
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<MenuVotingDbContext>()
.AddDefaultTokenProviders()
.AddUserStore<UserStore<ApplicationUser, ApplicationRole, MenuVotingDbContext, Guid>>()
.AddRoleStore<RoleStore<ApplicationRole, MenuVotingDbContext, Guid>>();

//JWT server-side authentication
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
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

builder.Services.AddScoped<IMenuVotingsService, MenuVotingsService>();
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
		policyBuilder.WithOrigins("https://localhost:4200")
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});

builder.Services.AddAuthorization();

var app = builder.Build();

// enable https
app.UseHsts();
// getting error with authorization cause UseRouting after UseAuthorization
app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

//UseAuthorization should be imported after UseCors() and UseHttpsRedirection() in order to avoid XMLHttpRequest has been blocked by Cors policy
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

app.Run();
