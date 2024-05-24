using GeekShopping.CouponAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponAPI.Model
{
    public class Coupon : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string CouponCode { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; }

    }
}
