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
        public string? ConnectionId { get; set; }
        [NotMapped]
        public IFormFile? iff_UserPhoto { get; set; }
        public bool IsCompany { get; set; }
        public Company? Company { get; set; }
        public PayCard? PayCard { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Rent>? Rents { get; set; }
        public List<Order> Orders { get; set; }
        public List<Chat> Chats { get; set; }
        public Wishlist Wishlist { get; set; }
        public User()
        {
            Comments = new();
            Rents = new();
            Orders = new();
            Chats = new();
        }
    }
}
