using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopAcces.Models;

namespace ShopAcces.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<UsersBD>UsersBD {  get; set; }
        public DbSet<Accessores>Accessores { get; set; }
        public DbSet<Admin>Admin { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
