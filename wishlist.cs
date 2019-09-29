using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GateBoys.Models
{
    public class wishlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Display(Name ="Product id")]
        public int productId { get; set; }
        [Display(Name = "Product Name")]
        public string productName { get; set; }
        public decimal price { get; set; }
        public string usermail { get; set; }
        public DateTime date { get; set; }
    }
}