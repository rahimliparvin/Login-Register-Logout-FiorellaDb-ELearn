
using ELEARN.Data;
using ELEARN.Models;
using ELEARN.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELEARN.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
    
        public async Task<IActionResult> Index()
        {

            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<Course> courses = await _context.Courses.Include(m => m.Author).Include(m => m.CourseImages).Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<Author> authors = await _context.Authors.Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<Event> events = await _context.Events.Where(m => !m.SoftDelete).ToListAsync();

            IEnumerable<News> news = await _context.News.Where(m => !m.SoftDelete).ToListAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                Courses = courses,
                Authors = authors,
                Events = events,
                News = news

            };


            return View(model);
        }

       
    }
}