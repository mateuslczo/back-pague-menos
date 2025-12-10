using Microsoft.EntityFrameworkCore;


using OrderDataManagement.Domain.Entities;

namespace OrderDataManagement.Infrastructure.Data
{
	/// <summary>
	/// Classe de contexto - EF Core
	/// </summary>
	public class EFDbContext :DbContext
	{
		public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
		{
		}

		public DbSet<Collaborator> Collaborators { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
	
		//	optionsBuilder.UseInMemoryDatabase("PagMenosDataBase");
		//}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Collaborator>(entity =>
			{
				entity.HasIndex(e => e.User).IsUnique();
				entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
				entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
				entity.Property(e => e.User).IsRequired().HasMaxLength(50);
				entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
			});

			// Order Configuration
			modelBuilder.Entity<Order>(entity =>
			{
				entity.HasIndex(e => e.OrderNumber).IsUnique();
				entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
				entity.Property(e => e.TotalOrder).HasPrecision(18, 2);
				entity.Property(e => e.OrderStatus).IsRequired().HasMaxLength(20);
				entity.Property(e => e.CreatedAt).HasConversion(typeof(DateTime));

				// Relacionamento
				entity.HasOne(o => o.Collaborator)
					  .WithMany(c => c.Orders)
					  .HasForeignKey(o => o.CollaboratorId)
					  .OnDelete(DeleteBehavior.Restrict);
			});


			modelBuilder.Entity<Product>(entity =>
			{
				entity.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
				entity.Property(e => e.Price).HasPrecision(18, 2);
				entity.Property(e => e.StockQuantity).HasConversion(typeof(decimal));
			});


			modelBuilder.Entity<OrderItem>(entity =>
			{
				entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
				entity.Property(e => e.ProductQuantity).IsRequired();

				// Relacionamento
				entity.HasOne(io => io.Order)
					  .WithMany(o => o.OrderItems)
					  .HasForeignKey(io => io.OrderId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(io => io.Product)
					  .WithMany(p => p.OrderItems)
					  .HasForeignKey(io => io.ProductId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasIndex(e => new { e.OrderId, e.ProductId }).IsUnique();
			});

			// Seed Data
			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, ProductName = "Fenergan Cetaconazona", StockQuantity = 100, Price = 29.99m },
				new Product { Id = 2, ProductName = "Penicilina Benzatina", StockQuantity = 50, Price = 49.99m },
				new Product { Id = 3, ProductName = "Creme Hidratante", StockQuantity = 200, Price = 9.99m }
			);

			modelBuilder.Entity<Collaborator>().HasData(
				new Collaborator { Id = 1, Name = "Marcos", LastName = "Mateus", User = "admin", Password = "1234" },
				new Collaborator { Id = 2, Name = "Lucenzo", LastName = "Mateus", User = "func", Password = "1234" }
			);
		}
	}
}