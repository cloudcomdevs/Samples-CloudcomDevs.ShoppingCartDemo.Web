using System.ComponentModel.DataAnnotations;

namespace CloudComDevs.ShoppingCartDemo.Web.Models
{
  public class CartItem
  {
    [Key]
    public int ItemId { get; set; }

    public string CartId { get; set; }

    public int Quantity { get; set; }


    public double? Amount { get; set; }

    public System.DateTime DateCreated { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; }

  }
}