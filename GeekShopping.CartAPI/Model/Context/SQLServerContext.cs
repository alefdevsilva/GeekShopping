using GeekShopping.ProductCart.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext() { }
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) {}
        public DbSet<Product> Products { get; set; }
    }
}
