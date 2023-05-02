using ELEARN.Data;
using ELEARN.Models;
using ELEARN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ELEARN.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAll() => await _context.Courses.Include(m => m.CourseImages).Include(m => m.Author).ToListAsync();

        public async Task<Course> GetById(int id) => await _context.Courses.FindAsync(id);

        public async Task<Course> GetFullDataById(int id) => await _context.Courses.Include(m => m.CourseImages)
                                                                                   .Include(M => M.Author)?.
                                                                                    FirstOrDefaultAsync(m => m.Id == id);
    }
        
}
