using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Add_TemplateAdmin.Models
{
    [Table("Orders")]
    public class Orders
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        public DateTime date { get; set; }
        //dau ? the hien allow null
        public double? price { get; set; }
        public int status { get; set; }
    }
}