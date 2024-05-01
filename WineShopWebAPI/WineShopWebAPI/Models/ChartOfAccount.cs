using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    public class ChartOfAccount
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AccountGroup")]
        public int GroupId { get; set; }

        public string Name { get; set; }

        public virtual AccountGroup AccountGroup { get; set; }
    }

}
