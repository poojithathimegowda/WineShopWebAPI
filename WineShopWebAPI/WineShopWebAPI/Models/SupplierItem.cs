using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    public class SupplierItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Item Item { get; set; }
    }

}
