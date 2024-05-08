using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    // Inventory Entity
    public class Inventory
    {
        [Key]
        public int Inventory_ID { get; set; }
        public int Product_ID { get; set; } // Foreign Key to Product Table
        public int Shop_ID { get; set; } // Foreign Key to Shop Table
        public int Quantity { get; set; }
        [ForeignKey("Product_ID")]
        public virtual Product Product { get; set; }
        [ForeignKey("Shop_ID")]
        public virtual Shop Shop { get; set; }
    }
    public class ShopInventory
    {
        public int Product_ID { get; set; } 
        public int Shop_ID { get; set; } 
        public int Quantity { get; set; }
        
    }
}
