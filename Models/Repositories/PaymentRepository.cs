using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudComDevs.ShoppingCartDemo.Web.Models.Repositories
{
    public class PaymentRepository : Repository<Payment>
    {
        public Payment GetPaymentForOrder(string id)
        {
            return DbSet.Where(a => a.ReferenceNumber == id).Take(1).SingleOrDefault();
        }
    }
}