using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Add_TemplateAdmin.Models
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        public int id { get; set; }
        public int parent_id { get; set; }
        public string name { get; set; }
        public int displayhome { get; set; }
    }
}