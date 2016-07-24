using System.ComponentModel.DataAnnotations;

namespace CloudComDevs.ShoppingCartDemo.Web.Models
{
  public class OrderDetail
  {
      [Key]
    public int OrderDetailId { get; set; }

    public string  OrderId { get; set; }

    public string Username { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public double? UnitPrice { get; set; }

  }
}