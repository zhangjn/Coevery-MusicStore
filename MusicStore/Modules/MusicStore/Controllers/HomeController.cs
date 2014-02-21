using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Coevery.Data;
using Coevery.Themes;
using MusicStore.Models;

namespace MusicStore.Controllers {
    [Themed]
    public class HomeController : Controller {
        private readonly IRepository<Album> _albumRepository;

        public HomeController(IRepository<Album> albumRepository) {
            _albumRepository = albumRepository;
        }

        public ActionResult Index() {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);
            return null;
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return _albumRepository.Table
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }
    }
}