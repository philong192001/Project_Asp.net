using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Add_TemplateAdmin.Models
{
        [Table("Customers")]
        public class Customers
        {
            [Key]
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string password { get; set; }
        }
 }
