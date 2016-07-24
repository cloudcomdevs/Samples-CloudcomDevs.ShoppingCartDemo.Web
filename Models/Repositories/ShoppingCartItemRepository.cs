using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudComDevs.ShoppingCartDemo.Web.Models.Repositories
{
    public class ShoppingCartItemRepository : Repository<CartItem>
    {
        
        string ShoppingCartId { get; set; }
       

        public ShoppingCartItemRepository(string shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
        }

      
        public void AddToCard(Product product, int quantity)
        {
            // check if produt already exists in the card
            var cartItem = DbSet.SingleOrDefault(a => a.CartId == ShoppingCartId && a.ProductId == product.ProductID);

            if (cartItem == null)
            {
               // product.Quantity = 1;
                // Create a new cart item if no product item exits
                cartItem = new CartItem
                {                    
                    CartId = ShoppingCartId,
                    ProductId = product.ProductID,
                    Quantity = quantity,
                    DateCreated = DateTime.Now,
                    Amount = product.UnitPrice * quantity
                };
                base.Add(cartItem);
                SaveChanges();
            }
            else
            {
                // If the item exist in the cart increast the quantity
                cartItem.Quantity = cartItem.Quantity + quantity;
                cartItem.Amount = cartItem.Amount + product.UnitPrice * quantity;
                this.Update(cartItem);
               
            }
            // Save changes
            
        }

        public override void Update(CartItem entity)
        {
            base.Update(entity);
            SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = DbSet.SingleOrDefault(a => a.CartId == ShoppingCartId & a.ItemId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                //update the cart with item count
                DbSet.Remove(cartItem);

                //if (cartItem.Count > 1)
                //{
                //    cartItem.Count--;
                //    itemCount = cartItem.Count;
                //}
                //else
                //{
                //    storeDB.Carts.Remove(cartItem);
                //}
                // Save changes
               SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = DbSet.Where(a => a.CartId == ShoppingCartId).ToList();
            using (ProductRepository repo = new ProductRepository())
            {
                foreach (var item in cartItems)
                {
                    var product = repo.Get(item.ProductId);
                    product.Sold = product.Sold + item.Quantity;
                    product.Remaining = product.Remaining - product.Sold;
                    repo.Update(product);

                }
            };
            DbSet.RemoveRange(cartItems);
            SaveChanges();
        }
        public List<CartItem> GetCartItems()
        {
            return DbSet.Where(a => a.CartId == ShoppingCartId).ToList();
        }
        public int GetItemCountInCart()
        {
            // Get the count of each item in the cart and sum them up
            int? count = DbSet.Where(a => a.CartId == ShoppingCartId).Count(); 
            return count ?? 0;
        }
        public double GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            double? total = DbSet.Where(a => a.CartId == ShoppingCartId).Sum(a => a.Amount);
            //(from cartItems in storeDB.Carts
            //                  where cartItems.CartId == ShoppingCartId
            //                  select (int?)cartItems.Count *
            //                  cartItems.Album.Price).Sum();

            return total ?? 0.0d;
        }
        //public int CreateOrder(Order order)
        //{
        //    decimal orderTotal = 0;

        //    var cartItems = GetCartItems();
        //    // Iterate over the items in the cart, 
        //    // adding the order details for each
        //    foreach (var item in cartItems)
        //    {
        //        var orderDetail = new OrderDetail
        //        {
        //            AlbumId = item.AlbumId,
        //            OrderId = order.OrderId,
        //            UnitPrice = item.Album.Price,
        //            Quantity = item.Count
        //        };
        //        // Set the order total of the shopping cart
        //        orderTotal += (item.Count * item.Album.Price);

        //        storeDB.OrderDetails.Add(orderDetail);

        //    }
        //    // Set the order's total to the orderTotal count
        //    order.Total = orderTotal;

        //    // Save the order
        //    storeDB.SaveChanges();
        //    // Empty the shopping cart
        //    EmptyCart();
        //    // Return the OrderId as the confirmation number
        //    return order.OrderId;
        //}
       
     
    }
}
