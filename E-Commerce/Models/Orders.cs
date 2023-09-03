using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; } 

        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
    }
}