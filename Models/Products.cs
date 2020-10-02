using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Add_TemplateAdmin.Models
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int category_id { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string photo { get; set; }
        public int hot { get; set; }
        public double price { get; set; }
        public int discount { get; set; }
    }
}