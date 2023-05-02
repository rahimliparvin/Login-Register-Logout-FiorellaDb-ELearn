using ELEARN.Areas.Admin.ViewModels;
using ELEARN.Data;
using ELEARN.Helpers;
using ELEARN.Models;
using ELEARN.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ELEARN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ICourseService _courseService;
        private readonly IAuthorService _authorService;
        public CourseController(AppDbContext context,
                                IWebHostEnvironment env,
                                ICourseService courseService,
                                IAuthorService authorService)
        {
            _context = context;
            _env = env;
            _courseService = courseService;
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Course> courses = await _context.Courses.Where(m => !m.SoftDelete).Include(m => m.Author).Include(m => m.CourseImages).ToListAsync();

            return View(courses);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.authors = await GetAuthors();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create(CourseCreateVM newCourse)
        {
           
            ViewBag.authors = await GetAuthors();

            if (!ModelState.IsValid)
            {
                return View(newCourse);
            }

            foreach (var photo in newCourse.Photos)
            {
                if (!photo.CheckFileType("image/"))
                {

                    ModelState.AddModelError("Photos", "File type must be image");
                    return View();

                }


                if (photo.CheckFileSize(200))
                {

                    ModelState.AddModelError("Photos", "Photo size must be max 200Kb");
                    return View();

                }

            }


            List<CourseImage> courseImages = new();


            foreach (var photo in newCourse.Photos)
            {

                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                string path = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);

                FileHelper.SaveFileAsync(path, photo);

                CourseImage courseImage = new()
                {
                    Image = fileName
                };

                courseImages.Add(courseImage);
            }

            courseImages.FirstOrDefault().IsMain = true;

            

            Course course = new()
            {
                Name = newCourse.Name,
                Description = newCourse.Description,
                Price = newCourse.Price,
                AuthorId = newCourse.AuthorId,
                CourseImages = courseImages
            };

            await _context.CourseImages.AddRangeAsync(courseImages);
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            if (id == null) return BadRequest();

            Course course = await _courseService.GetFullDataById(id);

            if (course == null) return NotFound();


            foreach (var item in course.CourseImages)
            {
                string path = FileHelper.GetFilePath(_env.WebRootPath, "images",item.Image );

                FileHelper.DeleteFile(path);
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            ViewBag.authors = await GetAuthors();

            Course dbCourse = await _courseService.GetFullDataById((int)id);

            if (dbCourse == null) return NotFound();


            CourseEditVM model = new()
            {
                Id = dbCourse.Id,
                Name = dbCourse.Name,
                Price = dbCourse.Price,
                AuthorId = dbCourse.AuthorId,
                Images = dbCourse.CourseImages,
                Description = dbCourse.Description
            };


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CourseEditVM updatedCourse)
        {
            if (id == null) return BadRequest();

            ViewBag.authors = await GetAuthors();

            Course dbCourse = await _context.Courses.AsNoTracking().Include(m => m.CourseImages).Include(m => m.Author).FirstOrDefaultAsync(m => m.Id == id);

            if (dbCourse == null) return NotFound();

            if (!ModelState.IsValid)
            {
                updatedCourse.Images = dbCourse.CourseImages;
                return View(updatedCourse);
            }

            List<CourseImage> courseImages = new();

            if (updatedCourse.Photos is not null)
            {
                foreach (var photo in updatedCourse.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "File type must be image");
                        updatedCourse.Images = dbCourse.CourseImages;
                        return View(updatedCourse);
                    }

                    if (photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photos", "Image size must be max 200kb");
                        updatedCourse.Images = dbCourse.CourseImages;
                        return View(updatedCourse);
                    }
                }



                foreach (var photo in updatedCourse.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);

                    CourseImage courseImage = new()
                    {
                        Image = fileName
                    };

                    courseImages.Add(courseImage);
                }

                await _context.CourseImages.AddRangeAsync(courseImages);
            }


            Course newCourse = new()
            {
                Id = dbCourse.Id,
                Name = updatedCourse.Name,
                Price = updatedCourse.Price,
                Description = updatedCourse.Description,
                AuthorId = updatedCourse.AuthorId,
                CourseImages = courseImages.Count == 0 ? dbCourse.CourseImages : courseImages
            };


            _context.Courses.Update(newCourse);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> DeleteCourseImage(int? id)
        {
            if (id == null) return BadRequest();

            bool result = false;

            CourseImage courseImage = await _context.CourseImages.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (courseImage == null) return NotFound();

            var data = await _context.Courses.Include(m => m.CourseImages).FirstOrDefaultAsync(m => m.Id == courseImage.CourseId);

            if (data.CourseImages.Count > 1)
            {
                string path = FileHelper.GetFilePath(_env.WebRootPath, "images", courseImage.Image);

                FileHelper.DeleteFile(path);

                _context.CourseImages.Remove(courseImage);

                await _context.SaveChangesAsync();

                result = true;
            }

            data.CourseImages.FirstOrDefault().IsMain = true;

            await _context.SaveChangesAsync();

            return Ok(result);

        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            Course course = await _context.Courses.Where(m => m.Id == id).Include(m => m.Author).Include(m => m.CourseImages).FirstOrDefaultAsync();

            if (course == null) return NotFound();

            CourseDetailVM courseDetailVM = new()
            {
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Images = course.CourseImages,
                AuthorName = course.Author.Name
            };


            return View(courseDetailVM);
        }


        public async Task<SelectList> GetAuthors()
        {
            IEnumerable<Author> authors = await _authorService.GetAll();
            return new SelectList(authors, "Id", "Name");
        }

    }
}
