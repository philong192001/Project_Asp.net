using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Add_TemplateAdmin.Models
{
    [Table("Rating")]
    public class Rating
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int star { get; set; }

    }
}
