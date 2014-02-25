using System.Linq;
using System.Web.Mvc;
using Coevery.Data;
using Coevery.Themes;
using MusicStore.Models;
using MusicStore.Services;
using MusicStore.ViewModels;

namespace MusicStore.Controllers {
    [Themed]
    public class ShoppingCartController : Controller {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IRepository<Album> albumRepository, IRepository<Cart> cartRepository, IShoppingCartService shoppingCartService) {
            _albumRepository = albumRepository;
            _cartRepository = cartRepository;
            _shoppingCartService = shoppingCartService;
        }

        //
        // GET: /ShoppingCart/

        public ActionResult Index() {
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel {
                CartItems = _shoppingCartService.GetCartItems(),
                CartTotal = _shoppingCartService.GetTotal()
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
            _shoppingCartService.AddToCart(addedAlbum);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5

        [HttpPost]
        public ActionResult RemoveFromCart(int id) {
            // Remove the item from the cart

            // Get the name of the album to display confirmation
            string albumName = _cartRepository.Table
                .Single(item => item.Id == id).Album.Title;

            // Remove from cart
            int itemCount = _shoppingCartService.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel {
                Message = Server.HtmlEncode(albumName) +
                          " has been removed from your shopping cart.",
                CartTotal = _shoppingCartService.GetTotal(),
                CartCount = _shoppingCartService.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        //
        // GET: /ShoppingCart/CartSummary

        [ChildActionOnly]
        public ActionResult CartSummary() {
            ViewData["CartCount"] = _shoppingCartService.GetCount();

            return PartialView("CartSummary");
        }
    }
}