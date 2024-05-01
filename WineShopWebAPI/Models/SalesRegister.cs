using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    public class SalesRegister
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public int QuantitySold { get; set; }
        public decimal AmountSold { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Item Item { get; set; }
    }

}
