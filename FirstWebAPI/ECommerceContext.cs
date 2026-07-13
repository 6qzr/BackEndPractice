using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystem
{
    public class ECommerceContext   :   DbContext
    {
        public DbSet<User>      Users       { get; set; }
        public DbSet<Category>  Categories  { get; set; }
        public DbSet<Product>   Products    { get; set; }
        public DbSet<Order>     Orders      { get; set; }
        public DbSet<OrderItem> OrderItems  { get; set; }
        public DbSet<Review>    Reviews     { get; set; }

        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
        }
    }
}
