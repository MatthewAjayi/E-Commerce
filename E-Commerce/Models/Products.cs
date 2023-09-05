using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        [DisplayName("Product Name")]
        [Required]
        public string ProductName { get; set; }

        [DisplayName("Cateogry")]
        public string Category { get; set; }

        [DisplayName("ProductDescription")]
        public string ProductDescription { get; set; }

        [Key]
        public int CategoryID { get; set; }

        //[DisplayName("Quantity")]
        //[Required]
        //public int Quantity { get; set; }

        //[DisplayName("Price")]
        //[Required]
        //public double Price { get; set; }
    }
}