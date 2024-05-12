using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineShopWebAPI.Models
{
    // Product Entity
    public class Product
    {
        [Key]
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int Supplier_ID { get; set; } // Foreign Key to Supplier Table
        [ForeignKey("Supplier_ID")]
        public virtual Supplier Supplier { get; set; }
    }

    public class ProductRequest
    {

        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int Supplier_ID { get; set; } // Foreign Key to Supplier Table

    }

    public class ProductWithSupplier
    {

        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string Supplier_Name { get; set; }
        public int Supplier_ID { get; set; } // Foreign Key to Supplier Table

    }
}
