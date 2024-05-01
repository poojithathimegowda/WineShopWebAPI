using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    public class ItemStock
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public int StockBalance { get; set; }

        public virtual Item Item { get; set; }
    }

}
