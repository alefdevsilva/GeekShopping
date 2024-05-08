using GeekShopping.ProductCart.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) {}
        public DbSet<ProductViewModel> Products { get; set; }
        public DbSet<CartDetailViewModel> CartDetails { get; set; }
        public DbSet<CartHeaderViewModel> CartHeaders { get; set; }
        
    }
}
