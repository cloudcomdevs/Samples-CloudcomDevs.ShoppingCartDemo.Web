using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace CloudComDevs.ShoppingCartDemo.Web.Models.Repositories
{
    public class OrderDetailsRepository : Repository<OrderDetail>
    {
        public override void Update(OrderDetail model)
        {
            base.Update(model);
            SaveChanges();

        }

       
    }
}