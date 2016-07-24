using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudComDevs.ShoppingCartDemo.Web.Models.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public List<Category> GetByName(String name)
        {            
            return DbSet.Where(a => a.CategoryName.Contains(name)).ToList();
        }

        public override void Update(Category model)
        {
            base.Update(model);
            SaveChanges();
            //entity.Version++;
            //base.Update(model);
            //SaveChanges();
        }
    }
}