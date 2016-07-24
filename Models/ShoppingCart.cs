using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudComDevs.ShoppingCartDemo.Web.Models
{
    public class ShoppingCart
    {

        [Key]
        
        public string CartId { get; set; }
        public int ItemCount { get; set; }
        public int CartOwner { get; set; }
        public double TotalCost { get; set; }
        [Column(TypeName = "DateTime2")]
        public System.DateTime DateCreated { get; set; }
        [Column(TypeName = "DateTime2")]
        public System.DateTime LastModifiedDate { get; set; }
        public virtual IList<CartItem> CartItems { get; set; }
    }
}