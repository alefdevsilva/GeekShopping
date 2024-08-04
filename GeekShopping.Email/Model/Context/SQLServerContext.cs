using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Model.Context
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) {}
        public DbSet<EmailLog> Emails { get; set; }
        
    }
}
