using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AD_Coursework.Models;
using Microsoft.AspNetCore.Identity;

namespace AD_Coursework.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<WhitelistItem> WhitelistedItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedRoles(modelBuilder);
            SeedAdminAndStaff(modelBuilder);

            // Configure the Announcement model
            modelBuilder.Entity<Announcement>()
                .Property(a => a.Type)
                .HasConversion<string>();

            // Configure the relationship between the User and Role models
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between User and Notification models
            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configures the relationship between Book and Publisher models
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configures the relationship between Book and Author models
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configures the relationship between Book and Genre models
            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookGenre>()
               .HasOne(bg => bg.Genre)
               .WithMany(g => g.BookGenres)
               .HasForeignKey(bg => bg.GenreId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });

            // Configures the relationship between User, Order, and OrderItem models.
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
               .HasMany(o => o.OrderItems)
               .WithOne(oi => oi.Order)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.OrderItems)
                .WithOne(oi => oi.Book)
                .HasForeignKey(oi => oi.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.BookId });

            // Configure the relationship between Cart, User, and CartItem models
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.CartItems)
                .WithOne(ci => ci.Book)
                .HasForeignKey(ci => ci.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.CartId, ci.BookId });

            // Configure the relationship between Book and Discount models
            modelBuilder.Entity<Discount>()
               .HasOne(d => d.Book)
               .WithMany(b => b.Discounts)
               .HasForeignKey(d => d.BookId)
               .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between User, WhitelistItem, and Book models
            modelBuilder.Entity<WhitelistItem>()
                .HasOne(w => w.Book)
                .WithMany(b => b.WhitelistItems)
                .HasForeignKey(w => w.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WhitelistItem>()
               .HasOne(w => w.User)
               .WithMany(u => u.WhitelistItems)
               .HasForeignKey(w => w.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between User, Review, and Book models
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the relationship between User and RefreshToken models
            modelBuilder.Entity<User>()
                .HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Id = Guid.Parse("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = Guid.Parse("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709"),
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = Guid.Parse("b8fd98e7-3a5b-4275-b4bb-8c7ccda0d6b0"),
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            };
            modelBuilder.Entity<Role>().HasData(roles);
        }

        private void SeedAdminAndStaff(ModelBuilder modelBuilder)
        {
            var adminId = Guid.NewGuid();
            var staffId = Guid.NewGuid();
            var adminRoleId = Guid.Parse("9f3b53b8-13e9-4c66-b45e-6c56d0b0d6db");
            var staffRoleId = Guid.Parse("a87642e3-e4cd-45f4-8c6d-1a1f5f6c6709");
            var hasher = new PasswordHasher<User>();

            var users = new List<User>
            {
                new User
                {
                    Id = adminId,
                    UserName = "admin@bookstore",
                    NormalizedUserName = "ADMIN@BOOKSTORE",
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "AdminBookStore@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = "1234567890",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FullName = "System Administrator",
                    Address = "Kathmandu, Nepal",
                    RegistrationDate = DateTime.UtcNow,
                    RoleId = adminRoleId
                },
                new User
                {
                    Id = staffId,
                    UserName = "staff@bookstore",
                    NormalizedUserName = "STAFF@BOOKSTORE",
                    Email = "staff@bookstore.com",
                    NormalizedEmail = "STAFF@BOOKSTORE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "StaffBookStore@123"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = "0987654321",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FullName = "Bookstore Staff",
                    Address = "Kathmandu, Nepal",
                    RegistrationDate = DateTime.UtcNow,
                    RoleId = staffRoleId
                }
            };

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}