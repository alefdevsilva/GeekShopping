using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext() { }
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) {}
        public DbSet<Product> Products { get; set; }
    }
}
