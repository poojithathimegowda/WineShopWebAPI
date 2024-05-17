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

    public class ShopModel
    {
        public int Shop_ID { get; set; }
        public string Shop_Name { get; set; }
        public string Location { get; set; }
    }

    public class OrderDto
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class ExpenseDto
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }

    public class ProfitLossDto
    {
        public ShopModel Shop { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public decimal ProfitOrLoss { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<ExpenseDto> ExpensesList { get; set; }
    }

}
