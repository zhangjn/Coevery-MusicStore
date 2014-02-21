﻿using System.Linq;
using System.Web.Mvc;
using Coevery;
using Coevery.Data;
using Coevery.Themes;
using MusicStore.Models;
using MusicStore.ViewModels;

namespace MusicStore.Controllers {
    [Themed]
    public class ShoppingCartController : Controller {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Cart> _cartRepository;

        public ShoppingCartController(IRepository<Album> albumRepository, IRepository<Cart> cartRepository, ICoeveryServices services) {
            _albumRepository = albumRepository;
            _cartRepository = cartRepository;
            Services = services;
        }

        public ICoeveryServices Services { get; set; }

        //
        // GET: /ShoppingCart/

        public ActionResult Index() {
            var cart = ShoppingCart.GetCart(this.HttpContext, Services);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5

        public ActionResult AddToCart(int id) {
            // Retrieve the album from the database
            var addedAlbum = _albumRepository.Table
                .Single(album => album.Id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext, Services);

            cart.AddToCart(addedAlbum);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5

        [HttpPost]
        public ActionResult RemoveFromCart(int id) {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext, Services);

            // Get the name of the album to display confirmation
            string albumName = _cartRepository.Table
                .Single(item => item.Id == id).Album.Title;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel {
                Message = Server.HtmlEncode(albumName) +
                          " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        //
        // GET: /ShoppingCart/CartSummary

        [ChildActionOnly]
        public ActionResult CartSummary() {
            var cart = ShoppingCart.GetCart(this.HttpContext, Services);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }
    }
}