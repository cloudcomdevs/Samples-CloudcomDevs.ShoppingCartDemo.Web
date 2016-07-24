using CloudComDevs.ShoppingCartDemo.Web.Models;
using CloudComDevs.ShoppingCartDemo.Web.Models.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace CloudComDevs.ShoppingCartDemo.Web.Controllers
{
    public class OrderController : Controller
    {
        ShippingRepository shippingRepository = new ShippingRepository();
        OrderRepository orderRepository = new OrderRepository();
        [Authorize]
        public ActionResult ShippingDetails()
        {
            IList<Shipping> ship = shippingRepository.GetByUser(WebSecurity.CurrentUserId);
            return View(ship);
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreateShipping()
        {
            //if (ModelState.IsValid)
            //{
            //    shippingRepository.Update(model);
            //    ViewBag.Message = "Shipping details updated successfully";
            //}
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateShipping(Shipping model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = WebSecurity.CurrentUserId;
                shippingRepository.Add(model);
                shippingRepository.SaveChanges();
                ViewBag.Message = "Shipping details saved successfully";
                ModelState.AddModelError("Custom", "Shipping details saved successfully");
            }
            // return View(model);
            return RedirectToAction("ShippingDetails");
        }

        [HttpGet]
        [Authorize]

        public ActionResult EditShipping(int Id)
        {
            Shipping shipping = shippingRepository.Get(Id);
            if (shipping == null)
            {
                ModelState.AddModelError("Custom", "No matching entry for given Id");
            }

            return View(shipping);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditShipping(Shipping model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = WebSecurity.CurrentUserId;
                shippingRepository.Update(model);
                ViewBag.Message = "Shipping details updated successfully";
                ModelState.AddModelError("Custom", "Shipping details updated successfully");
            }
            return RedirectToAction("ShippingDetails");
        }

        [Authorize]
        public ActionResult DeleteShipping(int id)
        {
            shippingRepository.Delete(id);
            shippingRepository.SaveChanges();
            ModelState.AddModelError("Custom", "Shipping details deleted successfully");
            return RedirectToAction("ShippingDetails");
        }


        [Authorize]
        // [ValidateAntiForgeryToken()]
        public ActionResult Confirm(int Id)
        {

            try
            {

                ShoppingCartRepository repository = new ShoppingCartRepository();
                ShoppingCart cart = repository.GetCartForUser(WebSecurity.CurrentUserId);


                string orderId = Guid.NewGuid().ToString();
                Order order = new Order
                {
                    userId = WebSecurity.CurrentUserId,
                    Username = WebSecurity.CurrentUserName,
                    Total = cart.TotalCost,
                    ShippingDetails_Id = Id,
                    OrderDate = DateTime.Now,
                    HasBeenShipped = false,
                    OrderId = orderId,

                };

                orderRepository.Add(order);
                orderRepository.SaveChanges();

                this.UpdateOrderDetails(orderId, cart.CartId);
                //string orderId =  orderRepository.ConfirmOrder(cart, cartItems, Id);
                //Order order =  orderRepository.Get(orderId);
                return RedirctToPayment(orderId, order.Total);
                //return View(order);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Custom", ex.Message);
            }

            return Redirect(Request.UrlReferrer.AbsoluteUri);

        }


        [HttpPost]
        public ActionResult RedirctToPayment(string orderId, double amount)
        {
            var collection = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            collection.Set("order", orderId);
            collection.Set("o", amount.ToString());
            collection.Set("return_url", Request.Url.AbsoluteUri);
            string server = string.Format(
                Request.Url.Host + "{0}", !string.IsNullOrEmpty(Request.Url.Port.ToString()) ? ":" +
                Request.Url.Port.ToString() : "");
            string gateway_path = ConfigurationManager.AppSettings["payment_gateway_url"].ToString();
            return new RedirectResult(gateway_path + "?" + collection.ToString());
        }


        public void UpdateOrderDetails(string orderId, string cartId)
        {
            ShoppingCartItemRepository itemRepository = new ShoppingCartItemRepository(cartId);
            IList<CartItem> cartItems = itemRepository.GetCartItems();
            // SaveChanges();
            //  Get CartItem => OrderDetails
            OrderDetailsRepository orderDetailRepo = new OrderDetailsRepository();
            foreach (var item in cartItems)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.UnitPrice

                };
                orderDetailRepo.Add(orderDetail);
                orderDetailRepo.SaveChanges();

            }





        }

        public async Task<ActionResult> Verification(string id)
        {
            int transactionId = 0;

            Thread.Sleep(3000);
            transactionId = await GetTransactionConfirmationAsync(id);

            if (transactionId > 0)
            {
                Order order = orderRepository.Get(id);
                order.PaymentTransactionId = transactionId;
                order.HasBeenShipped = true;
                orderRepository.Update(order);


                ShoppingCartRepository repository = new ShoppingCartRepository();
                repository.EmptyCart(WebSecurity.CurrentUserId);
                string CartSessionKey = "CartId";
                HttpContext.Session[CartSessionKey] = null;
                ViewBag.Message = "Thank you, your order is ready to be shipped";
                return View("index", order);
            }
            else
            {
                ModelState.AddModelError("customerror", "Unable to verify the payment process");
                return RedirectToAction("details", "shoppingcart");
            }
        }

        //Reduce the product quantity after pament success
        public void UpdateProduct()
        {
            OrderDetailsRepository repo = new OrderDetailsRepository();
        }

        public async Task<int> GetTransactionConfirmationAsync(string id)
        {
            string server = string.Format(Request.Url.Host + "{0}", !string.IsNullOrEmpty(Request.Url.Port.ToString()) ? ":" + Request.Url.Port.ToString() : "");
            string uri = "http://" + server + "/api/payment/" + id;

            using (HttpClient httpClient = new HttpClient())
            {

                return JsonConvert.DeserializeObject<int>(
                    await httpClient.GetStringAsync(uri)
                );
            }
        }
        //
        // GET: /Order/



        public ActionResult Index(Order model)
        {
            return View(model);
        }

        //
        // GET: /Order/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Order/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Order/Create

        [HttpPost]
        public ActionResult Create(Order model)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Order/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Order model)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Order/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Order/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, Order model)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
