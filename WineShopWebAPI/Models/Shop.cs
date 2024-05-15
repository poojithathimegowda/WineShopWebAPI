using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    // Shop Entity
    public class Shop
    {
        [Key]
        public int Shop_ID { get; set; }
        public string Shop_Name { get; set; }
        public string Location { get; set; }
        public virtual ICollection<User> Employees { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
       
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    public class ShopRequest
    {
        public int Shop_ID { get; set; }
        public string Shop_Name { get; set; }
        public string Location { get; set; }
    }

}
