﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MusicStore.Models {
    [Bind(Exclude = "Id")]
    public class Order {
        [ScaffoldColumn(false)]
        public virtual int Id { get; set; }

        [ScaffoldColumn(false)]
        public virtual DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public virtual string Username { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(160)]
        public virtual string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(160)]
        public virtual string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public virtual string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public virtual string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(40)]
        public virtual string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public virtual string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public virtual string Country { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        public virtual string Phone { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        [ScaffoldColumn(false)]
        public virtual decimal Total { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}