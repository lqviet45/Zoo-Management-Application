using Microsoft.EntityFrameworkCore;
using Entities.Models;

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

		public virtual DbSet<Meal> Meals { get; set; }

		public virtual DbSet<FeedingFood> FeedingFoods { get; set; }

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

			modelBuilder.Entity<Experience>()
				.HasMany(e => e.Skills)
				.WithMany(e => e.Experiences);
				

			modelBuilder.Entity<FeedingFood>().ToTable(nameof(FeedingFood));
			modelBuilder.Entity<Meal>().ToTable(nameof(Meal));
		}
	}
}
