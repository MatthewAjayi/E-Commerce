using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryID { get; set; }

        [Key]
        public int ProductID { get; set; }

        [DisplayName("Quantity")]
        [Required]
        public int Quantity { get; set; }

        [DisplayName("Price")]
        [Required]
        public decimal Price { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        //[DisplayName("Product Description")]
        //public string ProductDescription { get; set; }
    }
}