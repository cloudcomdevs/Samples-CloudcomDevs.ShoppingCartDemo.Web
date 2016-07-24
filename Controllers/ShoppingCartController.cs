using CloudComDevs.ShoppingCartDemo.Web.Filters;
using CloudComDevs.ShoppingCartDemo.Web.Models;
using CloudComDevs.ShoppingCartDemo.Web.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CloudComDevs.ShoppingCartDemo.Web.Controllers
{
    [Authorize]   
    public class ShoppingCartController : Controller
    {
        public const string CartSessionKey = "CartId";

        ShoppingCartRepository repository = new ShoppingCartRepository();

        private void UpdateSession(ShoppingCart cart, HttpContextBase context)
        {
            context.Session[CartSessionKey] = cart;
        }

        //HttpContextBase to allow access to cookies      
        private ShoppingCart GetUserCart(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                int userId = WebSecurity.GetUserId(context.User.Identity.Name);
                ShoppingCart cart = repository.GetCartForUser(userId); 

                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        CartId = Guid.NewGuid().ToString(),
                        CartOwner = WebSecurity.CurrentUserId,
                        DateCreated = DateTime.Now,
                        LastModifiedDate = DateTime.Now,
                        ItemCount = 0
                    };

                    repository.Add(cart);
                    repository.SaveChanges();
                    context.Session[CartSessionKey] = cart;
                }
                else
                {
                    context.Session[CartSessionKey] = cart;
                }

            }
            return (ShoppingCart)context.Session[CartSessionKey];
        }
        //
        // GET: /ShoppingCart/
        [Authorize]
        public PartialViewResult ShowCartSummary()
        {
           ShoppingCart cart= this.GetUserCart(HttpContext);
           return PartialView(cart);
        }
       
        public ActionResult Index()
        {
            
            return View();
        }

        //
        // GET: /ShoppingCart/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            id = this.GetUserCart(HttpContext).CartId;
            ShoppingCartItemRepository itemRepository = new ShoppingCartItemRepository(id);

            IList<CartItem> cartItems = itemRepository.GetCartItems();
           // if (cartItems.Count > 0)
            //{
                return View(cartItems);
           // }
            
        }

        [Authorize]
        public ActionResult Add(int Id, int quantity = 1)
        {
            ProductRepository productRepository = new ProductRepository();
            Product item = productRepository.Get(Id);
            ShoppingCart cart = this.GetUserCart(HttpContext);
            ShoppingCartItemRepository itemRepository = new ShoppingCartItemRepository(cart.CartId);
            itemRepository.AddToCard(item, quantity);
           // itemRepository.SaveChanges();

           cart = repository.UpdateShoppingCart(cart.CartId);

           this.UpdateSession(cart, HttpContext);

            return Redirect(Request.UrlReferrer.AbsoluteUri);

        }

        [Authorize]
        public void Remove(int id)
        {
           ShoppingCart cart = this.GetUserCart(HttpContext);
           ShoppingCartItemRepository itemRepository = new ShoppingCartItemRepository(cart.CartId);
           itemRepository.RemoveFromCart(id);
         //  cart = repository.UpdateShoppingCart(cart.CartId);
          //  this.UpdateSession(cart, HttpContext);
        }

     

        //
        // GET: /ShoppingCart/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            ShoppingCart cart = this.GetUserCart(HttpContext);
            ShoppingCartItemRepository itemRepository = new ShoppingCartItemRepository(cart.CartId);
            itemRepository.RemoveFromCart(id);
           // repository.UpdateShoppingCart(cart.CartId);
            cart = repository.UpdateShoppingCart(cart.CartId);
            this.UpdateSession(cart, HttpContext);
            //return View();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

    }
}
