using System.Linq;
using System.Web.Mvc;
using Coevery.Data;
using Coevery.Themes;
using MusicStore.Models;
using MusicStore.ViewModels;

namespace MusicStore.Controllers {
    //[Authorize(Roles = "Administrator")]
    [Themed]
    public class StoreManagerController : Controller {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<Artist> _artistRepository;

        public StoreManagerController(
            IRepository<Album> albumRepository,
            IRepository<Genre> genreRepository,
            IRepository<Artist> artistRepository) {
            _albumRepository = albumRepository;
            _genreRepository = genreRepository;
            _artistRepository = artistRepository;
        }

        //
        // GET: /StoreManager/

        public ViewResult Index() {
            var albums = _albumRepository.Table;
            return View(albums.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ViewResult Details(int id) {
            Album album = _albumRepository.Get(id);
            return View(album);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create() {
            ViewBag.GenreId = new SelectList(_genreRepository.Table, "Id", "Name");
            ViewBag.ArtistId = new SelectList(_artistRepository.Table, "Id", "Name");
            return View();
        }

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(AlbumViewModel album) {
            var newAlbum = new Album();
            TryUpdateModel(newAlbum);
            newAlbum.Genre = _genreRepository.Get(album.GenreId);
            newAlbum.Artist = _artistRepository.Get(album.ArtistId);

            if(ModelState.IsValid) {
                _albumRepository.Create(newAlbum);

                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(_genreRepository.Table, "Id", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_artistRepository.Table, "Id", "Name", album.ArtistId);
            return View(album);
        }

        //
        // GET: /StoreManager/Edit/5

        public ActionResult Edit(int id) {
            Album album = _albumRepository.Get(id);

            ViewBag.Genre_Id = new SelectList(_genreRepository.Table, "Id", "Name", album.Genre.Id);
            ViewBag.Artist_Id = new SelectList(_artistRepository.Table, "Id", "Name", album.Artist.Id);
            return View(album);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album) {
            if(ModelState.IsValid) {
                return RedirectToAction("Index");
            }
            ViewBag.Genre_Id = new SelectList(_genreRepository.Table, "Id", "Name", album.Genre.Id);
            ViewBag.Artist_Id = new SelectList(_artistRepository.Table, "Id", "Name", album.Artist.Id);
            return View(album);
        }

        //
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id) {
            Album album = _albumRepository.Get(id);
            return View(album);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            Album album = _albumRepository.Get(id);
            _albumRepository.Delete(album);

            return RedirectToAction("Index");
        }
    }
}