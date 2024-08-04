using GeekShopping.Email.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Email.Model
{
    public class EmailLog : BaseEntity
    {
        public string Email { get; set; }
        public string Log { get; set; }
        public DateTime SentDate { get; set; }
    }
}
