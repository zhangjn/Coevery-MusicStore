using System;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models {
    public class Cart {
        [Key]
        public virtual int Id { get; set; }

        public virtual string CartId { get; set; }
        public virtual int AlbumId { get; set; }
        public virtual int Count { get; set; }
        public virtual DateTime DateCreated { get; set; }

        public virtual Album Album { get; set; }
    }
}