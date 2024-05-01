using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    public class ExpenseClaim
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ChartOfAccount")]
        public int ChartOfAccountId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public string Description { get; set; }

        public virtual ChartOfAccount ChartOfAccount { get; set; }
        public virtual Customer Customer { get; set; }
    }

}
