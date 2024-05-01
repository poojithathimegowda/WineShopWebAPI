using System.ComponentModel.DataAnnotations;

namespace WineShopWebAPI.Models
{

    public class AccountGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ChartOfAccount> ChartOfAccounts { get; set; }
    }

}
