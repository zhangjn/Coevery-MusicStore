using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coevery;
using Coevery.Data;

namespace MusicStore.Models {
    public class ShoppingCart {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;

        public ShoppingCart(ICoeveryServices services) {
            _cartRepository = services.WorkContext.Resolve<IRepository<Cart>>();
            _orderDetailRepository = services.WorkContext.Resolve<IRepository<OrderDetail>>();
        }

        private string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context, ICoeveryServices services) {
            var cart = new ShoppingCart(services);
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller, ICoeveryServices services) {
            return GetCart(controller.HttpContext, services);
        }

        public void AddToCart(Album album) {
            // Get the matching cart and album instances
            var cartItem = _cartRepository.Table.SingleOrDefault(c => c.Cart_Id == ShoppingCartId && c.Album == album);

            if(cartItem == null) {
                // Create a new cart item if no cart item exists
                cartItem = new Cart {
                    Album = album,
                    Cart_Id = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };

                _cartRepository.Create(cartItem);
            }
            else {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }
        }

        public int RemoveFromCart(int id) {
            // Get the cart
            var cartItem = _cartRepository.Table.Single(
                cart => cart.Cart_Id == ShoppingCartId
                        && cart.Id == id);

            int itemCount = 0;

            if(cartItem != null) {
                if(cartItem.Count > 1) {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else {
                    _cartRepository.Delete(cartItem);
                }
            }

            return itemCount;
        }

        public void EmptyCart() {
            var cartItems = _cartRepository.Table.Where(cart => cart.Cart_Id == ShoppingCartId);

            foreach(var cartItem in cartItems) {
                _cartRepository.Delete(cartItem);
            }
        }

        public List<Cart> GetCartItems() {
            return _cartRepository.Table.Where(cart => cart.Cart_Id == ShoppingCartId).ToList();
        }

        public int GetCount() {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _cartRepository.Table
                where cartItems.Cart_Id == ShoppingCartId
                select (int?)cartItems.Count).Sum();

            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal() {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in _cartRepository.Table
                where cartItems.Cart_Id == ShoppingCartId
                select (int?)cartItems.Count * cartItems.Album.Price).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order) {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            // Iterate over the items in the cart, adding the order details for each
            foreach(var item in cartItems) {
                var orderDetail = new OrderDetail {
                    Album = item.Album,
                    Order = order,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Album.Price);

                _orderDetailRepository.Create(orderDetail);
            }

            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Empty the shopping cart
            EmptyCart();

            // Return the OrderId as the confirmation number
            return order.Id;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context) {
            if(context.Session[CartSessionKey] == null) {
                if(!string.IsNullOrWhiteSpace(context.User.Identity.Name)) {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName) {
            var shoppingCart = _cartRepository.Table.Where(c => c.Cart_Id == ShoppingCartId);

            foreach(Cart item in shoppingCart) {
                item.Cart_Id = userName;
            }
        }
    }
}