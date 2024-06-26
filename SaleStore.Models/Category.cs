﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SaleStore.Model
{
    public class Category
    {
        public Category()
        {
            Name = ""; // for making the annoying warning go away
        }
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)] // validations
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100")] // validations and error message
        public int DisplayOrder { get; set; }
    }
}
