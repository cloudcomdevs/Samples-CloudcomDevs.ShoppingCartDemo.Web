using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudComDevs.ShoppingCartDemo.Web.Models
{
  public class Order
  {
      [Key]
    public string OrderId { get; set; }

      [Column(TypeName = "DateTime2")]
    public System.DateTime OrderDate { get; set; }

    public int userId { get; set; }
    public string Username { get; set; }


    [ScaffoldColumn(false)]
    public double Total { get; set; }

    [ScaffoldColumn(false)]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentTransactionId { get; set; }

    [ScaffoldColumn(false)]
    public bool HasBeenShipped { get; set; }

    public int ShippingDetails_Id { get; set; }


    public List<OrderDetail> OrderDetails { get; set; }

    public virtual Shipping ShippingDetails { get; set; }
  }
}