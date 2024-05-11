using GeekShopping.CartAPI.Model.Base;
using GeekShopping.ProductCart.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartAPI.Model
{
    public class CartDetail : BaseEntity
    {
        public long CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }

        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int Count { get; set; }

    }
}
