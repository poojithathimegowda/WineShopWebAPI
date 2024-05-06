using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    // Expense Entity
    public class Expense
    {
        [Key]
        public int Expense_ID { get; set; }
        public int Shop_ID { get; set; } // Foreign Key to Shop Table
        public string Expense_Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Shop_ID")]
        public virtual Shop Shop { get; set; }
    }
}
