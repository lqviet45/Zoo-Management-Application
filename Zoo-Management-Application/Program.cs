using Microsoft.EntityFrameworkCore;
using Entities.AppDbContext;
using RepositoryContracts;
using Repositories;
using ServiceContracts;
using Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Zoo.Management.Application.Middleware;
using Zoo.Management.Application.Filters.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Add Services

builder.Services.AddScoped<IFileServices, FileServices>();

builder.Services.AddScoped<IUserRepositories, UserRepositories>();
builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IAreaRepositories, AreaRepositories>();
builder.Services.AddScoped<IAreaServices, AreaServices>();

builder.Services.AddScoped<ICageRepositories, CageRepositories>();
builder.Services.AddScoped<ICageServices, CageServices>();

builder.Services.AddScoped<ITicketReponsitories, TicketReponsitories>();
builder.Services.AddScoped<ITicketServices, TicketServices>();

builder.Services.AddScoped<ISpeciesRepositories, SpeciesRepositories>();
builder.Services.AddScoped<ISpeciesServices, SpeciesServices>();

builder.Services.AddScoped<IFoodRepositories, FoodRepositories>();	
builder.Services.AddScoped<IFoodServices, FoodServices>();

builder.Services.AddScoped<IMealRepositories, MealRepositories>();
builder.Services.AddScoped<IMealServices, MealServices>();

builder.Services.AddScoped<ICustommerReponsitories, CustommerReponsitories>();
builder.Services.AddScoped<ICustommerSevices, CustommerSevices>();

builder.Services.AddScoped<IOrderReponsitories, OrderReponsitories>();
builder.Services.AddScoped<IOrderSevices, OrderSevices>();

builder.Services.AddScoped<IEmailServices, EmailServices>();

builder.Services.AddScoped<INewsRepositories, NewsRepositories>();
builder.Services.AddScoped<INewsServices, NewsServices>();

builder.Services.AddScoped<INewsCategoriesRepositories, NewsCategoriesRepositories>();
builder.Services.AddScoped<INewsCategoriesServices, NewsCategoriesServices>();

builder.Services.AddScoped<IAnimalRepositories, AnimalRepositories>();
builder.Services.AddScoped<IAnimalServices, AnimalServices>();

builder.Services.AddScoped<IJwtServices, JwtServices>();

builder.Services.AddScoped<IAnimalUserRepositories, AnimalUserRepositories>();
builder.Services.AddScoped<IAnimalUserServices, AnimalUserServices>();

builder.Services.AddScoped<IAnimalCageRepositories, AnimalCageRepositories>();
builder.Services.AddScoped<IAnimalCageServices, AnimalCageServices>();

builder.Services.AddScoped<ISkillRepositories, SkillRepositories>();
builder.Services.AddScoped<ISkillServices, SkillServices>();

builder.Services.AddScoped<ValidationFilterAttribute>();


#endregion

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.WriteIndented = true;
	
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		Description = "Standard Authorization header using the bearer scheme (\"bearer {token}\")",
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});
	options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//CROS http://localhost:3000
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policyBuider =>
	{
		policyBuider
		.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
		.WithHeaders("Authorization", "origin", "accept", "content-type")
		.WithMethods("GET", "POST", "PUT", "DELETE")
		.AllowCredentials();
	});
});

// Add authentication to Server
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateAudience = true,
			ValidAudience = builder.Configuration["Jwt:Audience"],
			ValidateIssuer = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
				.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
		};
	});

builder.Services.AddAuthorization();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
else
{
	app.UseExceptionHandlingMiddleware();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
