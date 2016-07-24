using System.Data.Entity;

namespace CloudComDevs.ShoppingCartDemo.Web.Models
{

    public class PaymentContext : DbContext
    {
        public PaymentContext()
            : base("DefaultConnection")
        {
        }
       
    }
}