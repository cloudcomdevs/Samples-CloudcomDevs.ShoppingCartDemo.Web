using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudComDevs.ShoppingCartDemo.Web.Models
{
  public class Product
  {
      
    [ScaffoldColumn(false)]
      [Key]
    public int ProductID { get; set; }

    [Required, StringLength(100), Display(Name = "Name")]
    public string ProductName { get; set; }

    [Required, StringLength(10000), Display(Name = "Product Description"), DataType(DataType.MultilineText)]
    public string Description { get; set; }

    public string ImagePath { get; set; }
      [Display(Name="Product Image")]
    public byte[] Picture { get; set; }

    [Display(Name = "Price")]
    public double? UnitPrice { get; set; }

    public DateTime PostedDate { get; set; }

    public int Quantity { get; set; }
    public int Sold { get; set; }
    public int Remaining { get; set; }
    public int TotalViews { get; set; }
    public int Rating { get; set; }
    public int? CategoryID { get; set; }

    public virtual IList<Category> Category { get; set; }
    

  }
}