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
            modelBuilder.Entity<Users>()
            .HasDiscriminator<string>("UserType")  // Discriminator column
            .HasValue<Users>("User")
            .HasValue<Customers>("Customer")
            .HasValue<Sellers>("Seller")
            .HasValue<Administrator>("Admin");

            // Users Table Configuration
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.FirstName)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(u => u.LastName)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(u => u.Password)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(u => u.UserType)
                      .IsRequired();

                // One-to-one relationships with cascade delete
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
                      .HasForeignKey(c => c.UserId);

                entity.HasMany(u => u.Orders)
                      .WithOne(o => o.Users)
                      .HasForeignKey(o => o.UserId);

                entity.HasMany(u => u.Payments)
                      .WithOne(p => p.Users)
                      .HasForeignKey(p => p.UserId);

                entity.HasMany(u => u.Reviews)
                      .WithOne(r => r.Users)
                      .HasForeignKey(r => r.UserId);
                
            });

            // Customers Table Configuration
            modelBuilder.Entity<Customers>(entity =>
            {
                //entity.HasKey(c => c.CustomerId);

                entity.Property(c => c.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(c => c.Gender)
                      .IsRequired();

                entity.Property(c => c.DateOfBirth)
                      .IsRequired();

                entity.Property(c => c.AddressLine1)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(c => c.AddressLine2)
                      .HasMaxLength(255); // Optional

                entity.Property(c => c.Street)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(c => c.City)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(c => c.State)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(c => c.Country)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(c => c.PinCode)
                      .IsRequired()
                      .HasMaxLength(6)
                      .IsFixedLength(); // Ensures it's exactly 6 characters

                // Foreign Key Relationship
                entity.HasOne(c => c.Users)
                      .WithOne(u => u.Customers)
                      .HasForeignKey<Customers>(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Sellers Table Configuration
            modelBuilder.Entity<Sellers>(entity =>
            {
                //entity.HasKey(s => s.SellerId);

                entity.Property(s => s.SellerPhoneNumber)
                      .IsRequired();

                entity.Property(s => s.Gender)
                      .IsRequired();

                entity.Property(s => s.CompanyName)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(s => s.AddressLine1)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(s => s.AddressLine2)
                      .HasMaxLength(255); // Optional

                entity.Property(s => s.Street)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(s => s.City)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(s => s.State)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(s => s.Country)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(s => s.PinCode)
                      .IsRequired()
                      .HasMaxLength(6)
                      .IsFixedLength(); // Ensures it's exactly 6 characters

                entity.Property(s => s.GSTIN)
                      .IsRequired()
                      .HasMaxLength(15);

                entity.Property(s => s.BankAccountNumber)
                      .IsRequired()
                      .HasMaxLength(18);

                // Foreign Key Relationship
                entity.HasOne(s => s.Users)
                      .WithOne(u => u.Sellers)
                      .HasForeignKey<Sellers>(s => s.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // One-to-many relationship with Products
                entity.HasMany(s => s.Products)
                      .WithOne(p => p.Seller)
                      .HasForeignKey(p => p.SellerId);
            });

            // Admins Table Configuration
            modelBuilder.Entity<Administrator>(entity =>
            {
                //entity.HasKey(a => a.AdminId);

                entity.Property(a => a.AdminPhoneNumber)
                      .IsRequired();

                // Foreign Key Relationship with Users table
                entity.HasOne(a => a.Users)
                      .WithOne(u => u.Administrator)
                      .HasForeignKey<Administrator>(a => a.UserId) // Assuming UserId is the foreign key in Admins table
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Categories Table Configuration
            modelBuilder.Entity<Categories>(entity =>
            {
                // Primary Key Configuration
                entity.HasKey(c => c.CategoryId);

                // Column Configuration
                entity.Property(c => c.CategoryName)
                      .IsRequired()
                      .HasMaxLength(100); // Ensures category name is required and has a maximum length of 100 characters
            });

            // Products Table Configuration (assuming it has a foreign key to Categories)
            modelBuilder.Entity<Products>(entity =>
            {
                // Foreign Key Relationship with Categories table
                entity.HasOne(p => p.Categories)
                      .WithMany(c => c.Products)  // One category can have many products
                      .HasForeignKey(p => p.CategoryId) // Assuming CategoryId is the foreign key in Products table
                      .OnDelete(DeleteBehavior.SetNull);  // Adjust delete behavior as per business rules, e.g., SetNull on delete

                // Other Product configurations here (if necessary)
            });

            // Products Table Configuration
            modelBuilder.Entity<Products>(entity =>
            {
                // Primary Key Configuration
                entity.HasKey(p => p.ProductId);

                // Column Configuration
                entity.Property(p => p.ProductName)
                      .IsRequired()
                      .HasMaxLength(100); // Ensures the product name is required and has a maximum length of 100 characters

                entity.Property(p => p.ProductDescription)
                      .HasColumnType("TEXT"); // The product description is optional and is stored as TEXT

                entity.Property(p => p.ProductPrice)
                      .IsRequired()
                      .HasColumnType("DECIMAL(10, 2)"); // Product price with 10 digits, 2 after the decimal

                entity.Property(p => p.ProductStock)
                      .IsRequired();

                entity.Property(p => p.ProductUrl)
                      .HasMaxLength(255); // Optional product URL with a max length of 255 characters

                entity.Ignore(p => p.ProductImage);
                
                entity.Property(p => p.ProductImagePath) // Optional: configure path property if needed
                      .HasColumnName("ProductImagePath") // Optional: rename the column in the database
                      .HasMaxLength(255); // For storing the product image in binary format

                // Foreign Key Relationship with Sellers Table
                entity.HasOne(p => p.Seller)
                      .WithMany(s => s.Products)  // A seller can have many products
                      .HasForeignKey(p => p.SellerId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascading delete when a seller is deleted

                // Foreign Key Relationship with Categories Table
                entity.HasOne(p => p.Categories)
                      .WithMany(c => c.Products)  // A category can have many products
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascading delete when a category is deleted

                // Navigation Properties for related entities
                // One-to-many with ProductsInventory
                entity.HasMany(p => p.ProductsInventory)
                      .WithOne(pi => pi.Products)
                      .HasForeignKey(pi => pi.ProductId);

                // One-to-many with Cart
                entity.HasMany(p => p.Cart)
                      .WithOne(c => c.Products)
                      .HasForeignKey(c => c.ProductId);

                // One-to-many with Reviews
                entity.HasMany(p => p.Reviews)
                      .WithOne(r => r.Products)
                      .HasForeignKey(r => r.ProductId);
            });

            // ProductsInventory Table Configuration
            modelBuilder.Entity<ProductsInventory>(entity =>
            {
                // Primary Key Configuration
                entity.HasKey(pi => pi.InventoryId);

                // Column Configuration
                entity.Property(pi => pi.CurrentStock)
                      .IsRequired(); // Ensures that the current stock is required

                entity.Property(pi => pi.MinimumStock)
                      .IsRequired(); // Ensures that the minimum stock is required

                entity.Property(pi => pi.LastRestockDate)
                      .HasColumnType("DATETIME"); // Ensures LastRestockDate is a valid DATETIME type

                entity.Property(pi => pi.NextRestockDate)
                      .HasColumnType("DATETIME"); // Ensures NextRestockDate is a valid DATETIME type

                // Foreign Key Relationship with Products Table
                entity.HasOne(pi => pi.Products)
                      .WithMany(p => p.ProductsInventory)  // Assuming a one-to-one relationship between Products and ProductsInventory
                      .HasForeignKey(pi => pi.ProductId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascading delete when a product is deleted

            });

            // Configure Cart table
            modelBuilder.Entity<Cart>(entity =>
            {
                // Primary Key
                entity.HasKey(c => c.CartId);  // CartId is the primary key

                // Properties Configuration
                entity.Property(c => c.CartQuantity)
                      .HasDefaultValue(1);  // Default value for CartQuantity is 1

                entity.Property(c => c.Amount)
                      .IsRequired();  // Amount is required

                entity.Property(c => c.CreatedDate)
                      .HasDefaultValueSql("GETDATE()");  // Default value for CreatedAt is current date and time

                entity.Property(c => c.UpdatedDate)
                      .HasDefaultValueSql("GETDATE()");  // Default value for UpdatedAt is current date and time

                // Foreign Key Relationship to Users
                entity.HasOne(c => c.Users)  // Cart references Users
                      .WithMany()  // One user can have multiple carts
                      .HasForeignKey(c => c.UserId)  // Foreign key is UserId
                      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete when a user is deleted

                // Foreign Key Relationship to Products
                entity.HasOne(c => c.Products)  // Cart references Products
                      .WithMany()  // One product can be in multiple carts (assuming this relationship)
                      .HasForeignKey(c => c.ProductId)  // Foreign key is ProductId
                      .OnDelete(DeleteBehavior.NoAction);  // No action on delete of a product
            });

            // Configure Orders table
            modelBuilder.Entity<Orders>(entity =>
            {
                // Primary Key
                entity.HasKey(o => o.OrderId);  // OrderId is the primary key

                // Properties Configuration
                entity.Property(o => o.ItemQuantity)
                      .HasDefaultValue(1);  // Default value for ItemQuantity is 1

                entity.Property(o => o.UnitPrice)
                      .IsRequired();  // UnitPrice is required

                entity.Property(o => o.TotalAmount)
                      .IsRequired();  // TotalAmount is required

                entity.Property(o => o.ShippingAddress)
                      .IsRequired();  // ShippingAddress is required

                entity.Property(o => o.OrderDate)
                      .HasDefaultValueSql("GETDATE()");  // Default value for OrderDate is current date

                entity.Property(o => o.OrderStatus)
                      .HasConversion<int>();  // Default status is 'Pending'

                // Foreign Key Relationship to Users
                entity.HasOne(o => o.Users)  // Order references Users
                      .WithMany()  // One user can have many orders
                      .HasForeignKey(o => o.UserId)  // Foreign key is UserId
                      .OnDelete(DeleteBehavior.Restrict);  // Cascade delete when a user is deleted

                
                // Foreign Key Relationship to Products
                entity.HasOne(o => o.Products)  // Order references Products
                      .WithMany()  // One product can be in many orders
                      .HasForeignKey(o => o.ProductId)  // Foreign key is ProductId
                      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete when a product is deleted
            });

            // Configure Payments table
            modelBuilder.Entity<Payments>(entity =>
            {
                // Primary Key
                entity.HasKey(p => p.PaymentId);  // PaymentId is the primary key

                // Properties Configuration
                entity.Property(p => p.AmountToPay)
                      .IsRequired();  // AmountToPay is required

                entity.Property(p => p.PaymentMethod)
                      .HasConversion<int>(); // PaymentMethod is required

                entity.Property(p => p.PaymentStatus)
                      .HasConversion<int>(); // Default value for PaymentStatus is 'Pending'

                entity.Property(p => p.PaymentDate)
                      .HasDefaultValueSql("GETDATE()");  // Default value for PaymentDate is current date

                // Foreign Key Relationship to Orders
                entity.HasOne(p => p.Orders)  // Payment references Orders
                      .WithMany()  // One order can have many payments
                      .HasForeignKey(p => p.OrderId)  // Foreign key is OrderId
                      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete when an order is deleted

                // Foreign Key Relationship to Users
                entity.HasOne(p => p.Users)  // Payment references Users
                      .WithMany()  // One user can have many payments
                      .HasForeignKey(p => p.UserId)  // Foreign key is UserId
                      .OnDelete(DeleteBehavior.Restrict);  // Cascade delete when a user is deleted
            });

            // Configure Reviews table
            modelBuilder.Entity<Reviews>(entity =>
            {
                // Primary Key
                entity.HasKey(r => r.ReviewId);  // ReviewId is the primary key

                entity.HasCheckConstraint("CK_Rating", "rating >= 1 AND rating <= 5"); // Enforce rating range (1 to 5)

                // Properties Configuration
                entity.Property(r => r.Rating)
                      .IsRequired(); // Rating is required

                entity.Property(r => r.ReviewText)
                      .HasMaxLength(5000);  // Optional: Limit the length of ReviewText

                entity.Property(r => r.ReviewDate)
                      .HasDefaultValueSql("GETDATE()");  // Default value for ReviewDate is current date

                // Foreign Key Relationship to Users
                entity.HasOne(r => r.Users)  // Review references Users
                      .WithMany()  // One user can have many reviews
                      .HasForeignKey(r => r.UserId)  // Foreign key is UserId
                      .OnDelete(DeleteBehavior.Restrict);  // Cascade delete when a user is deleted

                // Foreign Key Relationship to Products
                entity.HasOne(r => r.Products)  // Review references Products
                      .WithMany()  // One product can have many reviews
                      .HasForeignKey(r => r.ProductId)  // Foreign key is ProductId
                      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete when a product is deleted
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
