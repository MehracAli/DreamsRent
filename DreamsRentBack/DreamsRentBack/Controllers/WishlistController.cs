using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Controllers
{
    public class WishlistController : Controller
    {
        public DreamsRentDbContext _context { get; set; }
        public WishlistController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult AddWishlist(int carId)
        {
            Car? car = _context.Cars
                .Include(c=>c.Brand).ThenInclude(b=>b.Models)
                    .Include(c=>c.CarPhotos)
                        .Include(c=>c.Company)
                            .FirstOrDefault(c=>c.Id == carId);

            User? user = _context.Users
                .Include(u=>u.Wishlist)
                    .ThenInclude(w=>w.wishlistItems)
                        .FirstOrDefault(u => u.UserName == User.Identity.Name);

            WishlistItem wishlistItem = new()
            {
                CarId = car.Id,
                CarName = car.Brand.Name + " " + car.Brand.Models.FirstOrDefault(m => m.Id == car.ModelId).Name,
                CarPhoto = car.CarPhotos.FirstOrDefault(p => p.CarId == car.Id).Path,
                Price = car.Price,
                CompanyName = car.Company.CompanyName
            };

            if (user.Wishlist != null) 
            {
                if (user.Wishlist.wishlistItems.Any(w => w.CarId == car.Id))
                {
                    WishlistItem wishlistItm = _context.WishlistItems.FirstOrDefault(w => w.Wishlist.Id == user.Wishlist.Id);
                    _context.WishlistItems.Remove(wishlistItm);

                    _context.SaveChanges();
                    return RedirectToAction("ConsumerAccount", "Account", new { UserName = user.UserName });
                }

                user.Wishlist.wishlistItems.Add(wishlistItem);


                _context.SaveChanges();
                return RedirectToAction("ConsumerAccount", "Account", new {UserName = user.UserName});
            }

            Wishlist newWishlist = new()
            {
                User = user,
            };

            newWishlist.wishlistItems.Add(wishlistItem);
            _context.Wishlists.Add(newWishlist);
            _context.SaveChanges();
            return RedirectToAction("ConsumerAccount", "Account", new { UserName = user.UserName });
        }

        public IActionResult RemoveWishItem(int Id)
        {
            WishlistItem deleteWishItem = _context.WishlistItems.FirstOrDefault(w=>w.Id == Id);

            User? user = _context.Users
                .Include(u => u.Wishlist)
                    .FirstOrDefault(u => u.UserName == User.Identity.Name);

            user.Wishlist.wishlistItems.Remove(deleteWishItem);
            _context.WishlistItems.Remove(deleteWishItem);
            _context.SaveChanges();
            return RedirectToAction("ConsumerAccount", "Account", new { UserName = user.UserName });
        }
    }
}
