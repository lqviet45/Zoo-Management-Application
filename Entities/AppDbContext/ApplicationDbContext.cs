using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Microsoft.Data.SqlClient;
using System;

namespace Entities.AppDbContext
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext() { }

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public virtual DbSet<Animal> Animals { get; set; }

		public virtual DbSet<Area> Areas { get; set; }

		public virtual DbSet<Cage> Cages { get; set; }

		public virtual DbSet<Experience> Experiences { get; set; }

		public virtual DbSet<Custommer> Custommers { get; set; }

		public virtual DbSet<Order> Orders { get; set; }

		public virtual DbSet<OrderDetail> OrderDetails { get; set; }

		public virtual DbSet<Role> Roles { get; set; }

		public virtual DbSet<Species> Species { get; set; }

		public virtual DbSet<Ticket> Tickets { get; set; }

		public virtual DbSet<User> Users { get; set; }

		public virtual DbSet<Skill> Skills { get; set; }

		public virtual DbSet<Food> Foods { get; set; }

		public virtual DbSet<AnimalFood> AnimalFoods { get; set; }

		public virtual DbSet<NewsCategories> NewsCategories { get; set; }
		public virtual DbSet<News> News { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Role>(entity =>
			{
				entity.HasKey(e => e.RoleId);
				entity.ToTable(nameof(Role));
			});
			modelBuilder.Entity<User>().ToTable(nameof(User));
			modelBuilder.Entity<Custommer>().ToTable(nameof(Custommer));
			modelBuilder.Entity<Order>().ToTable(nameof(Order));
			modelBuilder.Entity<Ticket>().ToTable(nameof(Ticket));
			modelBuilder.Entity<OrderDetail>(entity =>
			{
				entity.HasKey(e => new { e.OrderID, e.TicketId });
				entity.ToTable(nameof(OrderDetail));
			});
			modelBuilder.Entity<Area>().ToTable(nameof(Area));
			modelBuilder.Entity<Cage>().ToTable(nameof(Cage));
			modelBuilder.Entity<Species>().ToTable(nameof(Species));
			modelBuilder.Entity<Animal>().ToTable(nameof(Animal));

			modelBuilder.Entity<Experience>().ToTable(nameof(Experience));
			modelBuilder.Entity<Skill>().ToTable(nameof(Skill));

			modelBuilder.Entity<Food>().ToTable(nameof(Food));

			modelBuilder.Entity<AnimalFood>(entity =>
			{
				entity.HasKey(e => new { e.AnimalId, e.FoodId, e.FeedingTime });
				entity.ToTable(nameof(AnimalFood));
			});

			modelBuilder.Entity<NewsCategories>().ToTable(nameof(NewsCategories));
			modelBuilder.Entity<News>().ToTable(nameof(News));
		}
	}
}
