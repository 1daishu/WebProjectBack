using Microsoft.EntityFrameworkCore;
using Pod.Models;

namespace Pod.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAuth>()
                .HasOne(x => x.User).WithOne(x => x.UserAuth).HasForeignKey<User>(x => x.UserAuthId);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Cart).WithOne(x => x.User).HasForeignKey<Cart>(x => x.UserId);

           

            modelBuilder.Entity<User>()
                .HasMany(x=>x.Responses).WithOne(x=>x.User).HasForeignKey(x=>x.UserId);

            modelBuilder.Entity<CartProduct>()
                .HasOne(x => x.Product).WithMany(x => x.CartProducts).HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<CartProduct>()
               .HasOne(x => x.Cart).WithMany(x => x.CartProducts).HasForeignKey(x => x.CartId);
        }
    }
}
