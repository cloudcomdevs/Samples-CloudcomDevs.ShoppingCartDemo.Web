using System.Data.Entity;
namespace CloudComDevs.ShoppingCartDemo.Web.Models
{
  public class ProductContext : DbContext
  {
    public ProductContext()
          : base("DefaultConnection")
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> ShoppingCartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<ShoppingCart> Cart { get; set; }
    public DbSet<Payment> Payment { get; set; }

      //ai test
  }

}