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
        private readonly ITransactionManager _transactionManager;

        public StoreManagerController(
            IRepository<Album> albumRepository,
            IRepository<Genre> genreRepository,
            IRepository<Artist> artistRepository,
            ITransactionManager transactionManager) {
            _albumRepository = albumRepository;
            _genreRepository = genreRepository;
            _artistRepository = artistRepository;
            _transactionManager = transactionManager;
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
        public ActionResult Create(AlbumViewModel albumViewModel) {
            var album = new Album();
            if(ModelState.IsValid && TryUpdateModel(album)) {
                album.Genre = _genreRepository.Get(albumViewModel.GenreId);
                album.Artist = _artistRepository.Get(albumViewModel.ArtistId);
                _albumRepository.Create(album);
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(_genreRepository.Table, "Id", "Name", albumViewModel.GenreId);
            ViewBag.ArtistId = new SelectList(_artistRepository.Table, "Id", "Name", albumViewModel.ArtistId);
            return View(albumViewModel);
        }

        //
        // GET: /StoreManager/Edit/5

        public ActionResult Edit(int id) {
            Album album = _albumRepository.Get(id);

            ViewBag.GenreId = new SelectList(_genreRepository.Table, "Id", "Name", album.Genre.Id);
            ViewBag.ArtistId = new SelectList(_artistRepository.Table, "Id", "Name", album.Artist.Id);

            var viewModel = new AlbumViewModel(album);
            return View(viewModel);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, AlbumViewModel albumViewModel) {
            var album = _albumRepository.Get(id);
            if(ModelState.IsValid && TryUpdateModel(album)) {
                album.Genre = _genreRepository.Get(albumViewModel.GenreId);
                album.Artist = _artistRepository.Get(albumViewModel.ArtistId);
                return RedirectToAction("Index");
            }
            _transactionManager.Cancel();
            ViewBag.GenreId = new SelectList(_genreRepository.Table, "Id", "Name", albumViewModel.GenreId);
            ViewBag.ArtistId = new SelectList(_artistRepository.Table, "Id", "Name", albumViewModel.ArtistId);
            return View(albumViewModel);
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