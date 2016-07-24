using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComDevs.ShoppingCartDemo.Web.Models
{
  public class Category
  {
    [Key]
    public int CategoryID { get; set; }

    [Required, StringLength(100), Display(Name = "Name")]
    public string CategoryName { get; set; }

    [Display(Name = "Product Description")]
    public string Description { get; set; }

    public virtual IList<Product> Products { get; set; }
  }
}