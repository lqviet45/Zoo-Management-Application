using Microsoft.EntityFrameworkCore;
using Entities.AppDbContext;
using RepositoryContracts;
using Repositories;
using ServiceContracts;
using Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Add Services

builder.Services.AddScoped<IFileServices, FileServices>();

builder.Services.AddScoped<IUserRepositories, UserRepositories>();
builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddScoped<IAreaRepositories, AreaRepositories>();
builder.Services.AddScoped<IAreaServices, AreaServices>();

builder.Services.AddScoped<IExperienceServices, ExperienceServices>();
builder.Services.AddScoped<IExperienceRepositories, ExperienceRepositories>();

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
#endregion

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
