using System.ComponentModel.DataAnnotations;

namespace WineShopWebAPI.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalesPrice { get; set; }
        public int ReOrderLevel { get; set; }
    }

}
