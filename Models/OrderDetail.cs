using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Add_TemplateAdmin.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        //dau ? the hien allow null
        public double? price { get; set; }
    }
}