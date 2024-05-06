using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WineShopWebAPI.Models
{

    // Supplier Entity
    public class Supplier
    {
        [Key]
        public int Supplier_ID { get; set; }
        public string Supplier_Name { get; set; }
        public string Contact_Details { get; set; }
    }
}
