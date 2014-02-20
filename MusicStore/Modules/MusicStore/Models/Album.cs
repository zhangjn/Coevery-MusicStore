using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models {
    public class Album {
        [ScaffoldColumn(false)]
        public virtual int Id { get; set; }

        [DisplayName("Genre")]
        public virtual int GenreId { get; set; }

        [DisplayName("Artist")]
        public virtual int ArtistId { get; set; }

        [Required(ErrorMessage = "An Album Title is required")]
        [StringLength(160)]
        public virtual string Title { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100.00,
            ErrorMessage = "Price must be between 0.01 and 100.00")]
        public virtual decimal Price { get; set; }

        [DisplayName("Album Art URL")]
        [StringLength(1024)]
        public virtual string AlbumArtUrl { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}