﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coevery.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<Artist> _artistRepository;

        //
        // GET: /StoreManager/

        public ViewResult Index()
        {
            var albums = _albumRepository.Table;
            return View(albums.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ViewResult Details(int id)
        {
            Album album = _albumRepository.Get(id);
            return View(album);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(_genreRepository.Table, "Id", "Name");
            ViewBag.ArtistId = new SelectList(_artistRepository.Table, "Id", "Name");
            return View();
        } 

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                _albumRepository.Create(album);

                return RedirectToAction("Index");  
            }

            ViewBag.GenreId = new SelectList(_genreRepository.Table, "Id", "Name", album.Id);
            ViewBag.ArtistId = new SelectList(_artistRepository.Table, "Id", "Name", album.Id);
            return View(album);
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Album album = _albumRepository.Get(id);
            ViewBag.GenreId = new SelectList(_genreRepository.Table, "Id", "Name", album.Id);
            ViewBag.ArtistId = new SelectList(_artistRepository.Table, "Id", "Name", album.Id);
            return View(album);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(_genreRepository.Table, "Id", "Name", album.Id);
            ViewBag.ArtistId = new SelectList(_artistRepository.Table, "Id", "Name", album.Id);
            return View(album);
        }

        //
        // GET: /StoreManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Album album = _albumRepository.Get(id);
            return View(album);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = _albumRepository.Get(id);
            _albumRepository.Delete(album);

            return RedirectToAction("Index");
        }
    }
}