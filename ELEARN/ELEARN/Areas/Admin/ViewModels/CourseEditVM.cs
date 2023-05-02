using ELEARN.Models;
using System.ComponentModel.DataAnnotations;

namespace ELEARN.Areas.Admin.ViewModels
{
    public class CourseEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public ICollection<CourseImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
