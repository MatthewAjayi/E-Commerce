﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
    }
}