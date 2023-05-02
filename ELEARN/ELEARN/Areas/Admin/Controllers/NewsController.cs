using ELEARN.Data;
using ELEARN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELEARN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;
        public NewsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<News> news = await _context.News.Where(m => !m.SoftDelete).ToListAsync();


            return View(news);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            News news = await _context.News.Where(m => m.Id == id).FirstOrDefaultAsync();

            if(news == null) return NotFound();

            return View(news);
        }

        public IActionResult Create()
        {


            return View();
        }
    }
}
