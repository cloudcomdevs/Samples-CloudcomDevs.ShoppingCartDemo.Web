using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudComDevs.ShoppingCartDemo.Web.Models.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        public List<Product> GetByName(String name)
        {
            return DbSet.Where(a => a.ProductName.Contains(name)).ToList();
        }

        public List<Product> GetLatestItems()
        {
            return DbSet.OrderBy(a => a.PostedDate).Take(10).ToList();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return DbSet.Where(a => a.CategoryID == categoryId).ToList();
        }
        public override void Update(Product model)
        {
            base.Update(model);
            SaveChanges();
            //entity.Version++;
            //base.Update(model);
            //SaveChanges();
        }
    }
}