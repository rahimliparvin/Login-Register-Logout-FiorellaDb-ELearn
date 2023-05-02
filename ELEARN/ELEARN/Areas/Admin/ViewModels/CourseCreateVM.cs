using ELEARN.Models;
using System.ComponentModel.DataAnnotations;

namespace ELEARN.Areas.Admin.ViewModels
{
    public class CourseCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
