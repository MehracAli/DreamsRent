using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Utilities.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodyTypeController : Controller
    {
        public DreamsRentDbContext _context { get; set; }
        private readonly IWebHostEnvironment _env;

        public BodyTypeController(DreamsRentDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult BodyTypes()
        {
            List<Body> bodyList = _context.Bodys.ToList();

            return View(bodyList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Body createBody)
        {
            Body body = new()
            {
                Name = createBody.Name,
            };

            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "index");
            body.BodyPhoto = await createBody.iff_BodyPhoto.CreateImage(imageFolderPath, "bodies");

            _context.Bodys.Add(body);
            _context.SaveChanges();
            return RedirectToAction(nameof(BodyTypes));
        }

        public IActionResult Edit(int Id)
        {
            Body body = _context.Bodys.FirstOrDefault(b => b.Id == Id);

            return View(body);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Body editBody)
        {
            Body body = _context.Bodys.FirstOrDefault(b => b.Id == editBody.Id);

            body.Name = editBody.Name;

            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "index");

            string removePath = Path.Combine(_env.WebRootPath, "assets", "images", "index", "bodies", body.BodyPhoto);

            body.BodyPhoto = await editBody.iff_BodyPhoto.CreateImage(imageFolderPath, "bodies");

            FileUpload.DeleteImage(removePath);

            _context.SaveChanges();
            return RedirectToAction(nameof(BodyTypes));
        }

    }
}
