using CloudComDevs.ShoppingCartDemo.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CloudComDevs.ShoppingCartDemo.Web.Models.Repositories
{
    public class ShoppingCartRepository :Repository<ShoppingCart>
    {
      

        //public ShoppingCart GetUserCart(HttpContextBase context)
        //{
        //    var cart = this.GetCart(context);
        //  //  cart.CartId = GetCartId(context);
        //    return cart;
        //}

        // Helper method to simplify shopping cart calls
        //public static ShoppingCart GetCart(Controller controller)
        //{
        //    return GetCart(controller.HttpContext);
        //}

        public ShoppingCart GetCartForUser(int userId)
        {
           return DbSet.SingleOrDefault(a => a.CartOwner == userId);
        }


        public void EmptyCart(int userId)
        {
            ShoppingCart cart = GetCartForUser(userId);
            ShoppingCartItemRepository r = new ShoppingCartItemRepository(cart.CartId);
            r.EmptyCart();
            DbSet.Remove(GetCartForUser(userId));
            SaveChanges();
            

        }

        public ShoppingCart UpdateShoppingCart(string cartId)
        {
             
            
            ShoppingCart cart = new ShoppingCart
            {
                CartId = cartId,
                CartOwner = WebSecurity.CurrentUserId,
                ItemCount = new ShoppingCartItemRepository(cartId).GetItemCountInCart(),
                TotalCost = new ShoppingCartItemRepository(cartId).GetTotal(),
                LastModifiedDate = DateTime.Now
            };
            
            Update(cart);
            return cart;
        }

        public override void Update(ShoppingCart entity)
        {
            base.Update(entity);
            SaveChanges();
        }

      
    }
}