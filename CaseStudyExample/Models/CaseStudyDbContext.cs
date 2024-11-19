using Microsoft.EntityFrameworkCore;

namespace CaseStudyExample.Models
{
    public class CaseStudyDbContext : DbContext
    {
        public CaseStudyDbContext() { }
        public CaseStudyDbContext(DbContextOptions<CaseStudyDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Users> Users { get; set; } = null!;
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Sellers> Sellers { get; set; } = null!;
        public virtual DbSet<Administrator> Administrator { get; set; } = null!;
        public virtual DbSet<Categories> Categories { get; set; } = null!;
        public virtual DbSet<Products> Products { get; set; } = null!;
        public virtual DbSet<ProductsInventory> ProductsInventory { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Orders> Orders { get; set; } = null!;
        public virtual DbSet<Payments> Payments { get; set; } = null!;
        public virtual DbSet<Reviews> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User entity configuration
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(u => u.UserId); // Primary Key

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Password)
                    .HasMaxLength(20);

                entity.Property(u => u.UserType)
                    .IsRequired();

                // One-to-one relationships
                entity.HasOne(u => u.Customers)
                    .WithOne(c => c.Users)
                    .HasForeignKey<Customers>(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.Sellers)
                    .WithOne(s => s.Users)
                    .HasForeignKey<Sellers>(s => s.SellerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.Administrator)
                    .WithOne(a => a.Users)
                    .HasForeignKey<Administrator>(a => a.AdminId)
                    .OnDelete(DeleteBehavior.Cascade);

                // One-to-many relationships
                entity.HasMany(u => u.Cart)
                    .WithOne(c => c.Users)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Orders)
                    .WithOne(o => o.Users)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Payments)
                    .WithOne(p => p.Users)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Reviews)
                    .WithOne(r => r.Users)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Additional configurations for related entities if needed
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(c => c.CustomerId);
            });

            modelBuilder.Entity<Sellers>(entity =>
            {
                entity.HasKey(s => s.SellerId);
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(a => a.AdminId);
            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var configSection = configBuilder.GetSection("ConnectionStrings");
            var conStr = configSection["conStr"] ?? null;

            optionsBuilder.UseSqlServer(conStr);

            //optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=EFCoreDemo;" +
            //"Trusted_Connection=True;TrustServerCertificate=True;");

            //base.OnConfiguring(optionsBuilder);
        }

    }
}
