using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
   

    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; }

        // Additional properties based on roles
        public virtual ICollection<Shop> OwnedShops { get; set; }
        public int? Shop_ID { get; set; }
        [ForeignKey("Shop_ID")]
        public virtual Shop Shop { get; set; }
    }

    //public class UserModel
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string Role { get; set; }
    //    public int ShopId { get; set; }
    //    public string UserName { get; set; }

    //    public string Email { get; set; }
    //}

}
