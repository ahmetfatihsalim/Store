﻿using System.ComponentModel.DataAnnotations;

namespace SaleStore.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}