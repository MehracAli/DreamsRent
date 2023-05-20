﻿using DreamsRentBack.Entities.CarModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamsRentBack.Entities.ClientModels
{
    public class Company:BaseEntity
    {
        public string CompanyName { get; set; }
        public int? Rating { get; set; } = 0;
        public bool? Verification { get; set; } = false;
        public string? CompanyPhoto { get; set; }
        [NotMapped]
        public IFormFile? iff_CompanyPhoto { get; set; }
        public int? LocationId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Location? Location { get; set; }
        public List<Car> Cars { get; set; }

        public Company()
        {
            Cars = new();
        }

    }
}