using DreamsRentBack.Entities.CarModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamsRentBack.Entities.ClientModels
{
    public class User:IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserPhoto { get; set; }
        [NotMapped]
        public IFormFile? iff_UserPhoto { get; set; }
        public bool IsCompany { get; set; }
        public Company? Company { get; set; }
        public PayCard? PayCard { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public User()
        {
            Comments = new();
            Likes = new();
        }
    }
}
