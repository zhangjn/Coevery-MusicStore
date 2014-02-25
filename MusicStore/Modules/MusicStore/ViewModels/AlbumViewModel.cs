using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MusicStore.Models;

namespace MusicStore.ViewModels {
    [Bind(Exclude = "Id")]
    public class AlbumViewModel {
        public AlbumViewModel() {}

        public AlbumViewModel(Album album) {
            Id = album.Id;
            GenreId = album.Genre.Id;
            ArtistId = album.Artist.Id;
            Title = album.Title;
            Price = album.Price;
            AlbumArtUrl = album.AlbumArtUrl;
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Genre")]
        public int GenreId { get; set; }

        [DisplayName("Artist")]
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "An Album Title is required")]
        [StringLength(160)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Price is required")]
        //[Range(0.01, 100.00,
        //    ErrorMessage = "Price must be between 0.01 and 100.00")]
        public decimal Price { get; set; }

        [DisplayName("Album Art URL")]
        [StringLength(1024)]
        public string AlbumArtUrl { get; set; }
    }
}