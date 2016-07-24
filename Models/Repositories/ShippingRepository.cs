using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudComDevs.ShoppingCartDemo.Web.Models.Repositories
{
    public class ShippingRepository :  Repository<Shipping>
    {

        public IList<Shipping> GetByUser(int UserId)
        {
            return DbSet.Where(a => a.UserId == UserId).ToList();
        }
        public override void Update(Shipping model)
        {
            base.Update(model);
            SaveChanges();
           
        }
    }
}
