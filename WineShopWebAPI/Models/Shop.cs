﻿using System.ComponentModel.DataAnnotations;

namespace WineShopWebAPI.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
    }

}