using System;
using System.Linq;
using System.Web.Mvc;
using Coevery.Data;
using Coevery.Themes;
using MusicStore.Models;
using MusicStore.Services;

namespace MusicStore.Controllers {
    //[Authorize]
    [Themed]
    public class CheckoutController : Controller {
        private readonly IRepository<Order> _orderRepository;
        private readonly IShoppingCartService _shoppingCartService;
        private const string PromoCode = "FREE";

        public CheckoutController(IRepository<Order> orderRepository, IShoppingCartService shoppingCartService) {
            _orderRepository = orderRepository;
            _shoppingCartService = shoppingCartService;
        }

        //
        // GET: /Checkout/AddressAndPayment

        public ActionResult AddressAndPayment() {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values) {
            var order = new Order();
            TryUpdateModel(order);

            try {
                if(string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false) {
                    return View(order);
                }
                else {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    _orderRepository.Create(order);

                    //Process the order
                    _shoppingCartService.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new {id = order.Id});
                }
            }
            catch {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        //
        // GET: /Checkout/Complete

        public ActionResult Complete(int id) {
            // Validate customer owns this order
            bool isValid = _orderRepository.Table.Any(
                o => o.Id == id &&
                     o.Username == User.Identity.Name);

            if(isValid) {
                return View(id);
            }
            else {
                return View("Error");
            }
        }
    }
}