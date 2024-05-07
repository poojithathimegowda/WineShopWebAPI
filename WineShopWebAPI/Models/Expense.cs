using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
 


    public class Expense
    {
        [Key]
        public int Expense_ID { get; set; }

        [ForeignKey("Shop_ID")]
        public int Shop_ID { get; set; } // Foreign Key to Shop Table

        public string Expense_Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        [JsonIgnore]
        public virtual Shop Shop { get; set; }
    }



    public class ShopExpenses
    {
        public int Shop_ID { get; set; } 
        public string Expense_Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
