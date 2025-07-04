﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Repository.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string Line1 { get; set; }
        public string? Line2 { get; set; }

        public string Country { get; set; }
        public bool IsCurrent { get; set; }
    }
}
