using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    // Order Entity
    public class Order
    {
        [Key]
        public int Order_ID { get; set; }
        public int Shop_ID { get; set; } // Foreign Key to Shop Table
        public int Product_ID { get; set; } // Foreign Key to Product Table
        public int Quantity { get; set; }
        public decimal Total_Amount { get; set; }
        public DateTime Order_Date { get; set; }
        [ForeignKey("Shop_ID")]
        public virtual Shop Shop { get; set; }
        [ForeignKey("Product_ID")]
        public virtual Product Product { get; set; }
    }
}
