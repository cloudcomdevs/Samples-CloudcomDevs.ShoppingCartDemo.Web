using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace CloudComDevs.ShoppingCartDemo.Web.Models.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        public override void Update(Order model)
        {
            base.Update(model);
            SaveChanges();

        }

        //public string ConfirmOrder(ShoppingCart cart, IList<CartItem> cartItemList, int shippingId)
        //{

        //    string orderId = Guid.NewGuid().ToString();
        //    //Get Cate ==> Order
        //    Order order = new Order
        //    {
        //        userId = WebSecurity.CurrentUserId,
        //        Username = WebSecurity.CurrentUserName,
        //        Total = cart.TotalCost,
        //        ShippingDetails = new Shipping { Id = shippingId },
        //        OrderDate = DateTime.Now,
        //        HasBeenShipped = false,
        //        OrderId = orderId
        //    };

        //    DbSet.Add(order);
        //}
        
    }
}