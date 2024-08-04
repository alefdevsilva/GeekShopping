using Microsoft.EntityFrameworkCore;
using GeekShopping.Email.Model.Context;
using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;

namespace GeekShopping.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<SQLServerContext> _context;

        public EmailRepository(DbContextOptions<SQLServerContext> context)
        {
            _context = context;
        }

        public async Task LogEmail(UpdatePaymentResultMessage updatePaymentResultMessage)
        {
            var email = new EmailLog
            {
                Email = updatePaymentResultMessage.Email,
                SentDate = DateTime.UtcNow,
                Log = $"Order - {updatePaymentResultMessage.OrderId} has been created successfully!"
            };

            await using var _db = new SQLServerContext(_context);
            _db.Emails.Add(email);
            await _db.SaveChangesAsync();
        }
    }
}
