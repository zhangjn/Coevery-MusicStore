using System.Collections.Generic;
using System.Web.Mvc;

namespace MusicStore.Models {
    [Bind(Exclude = "Id")]
    public class Album {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual decimal Price { get; set; }

        public virtual string AlbumArtUrl { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual IList<OrderDetail> OrderDetails { get; set; }
    }
}