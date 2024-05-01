using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    public class PurchaseRegister
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        [ForeignKey("ChartOfAccount")]
        public int ChartOfAccountId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public int QuantityPurchased { get; set; }
        public decimal UnitPricePurchased { get; set; }
        public decimal PurchaseAmount { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ChartOfAccount ChartOfAccount { get; set; }
        public virtual Item Item { get; set; }
    }

}
