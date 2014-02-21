using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coevery.Data;
using MusicStore.Models;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Genre> _genreRepository;

        //
        // GET: /Store/

        public ActionResult Index()
        {
            var genres = _genreRepository.Table.ToList();

            return View(genres);
        }

        //
        // GET: /Store/Browse?genre=Disco

        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            var genreModel = _genreRepository.Table
                .Single(g => g.Name == genre);

            return View(genreModel);
        }

        //
        // GET: /Store/Details/5

        public ActionResult Details(int id)
        {
            var album = _albumRepository.Get(id);

            return View(album);
        }

        //
        // GET: /Store/GenreMenu

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = _genreRepository.Table.ToList();

            return PartialView(genres);
        }

    }
}