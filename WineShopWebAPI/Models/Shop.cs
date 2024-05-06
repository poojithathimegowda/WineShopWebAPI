﻿using System.ComponentModel.DataAnnotations;

namespace WineShopWebAPI.Models
{
    // Shop Entity
    public class Shop
    {
        [Key]
        public int Shop_ID { get; set; }
        public string Shop_Name { get; set; }
        public string Location { get; set; }
        public virtual ICollection<User> Employees { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
