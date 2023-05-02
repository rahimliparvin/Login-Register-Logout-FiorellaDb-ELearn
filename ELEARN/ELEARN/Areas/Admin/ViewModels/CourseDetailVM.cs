using ELEARN.Models;

namespace ELEARN.Areas.Admin.ViewModels
{
    public class CourseDetailVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string AuthorName { get; set; }
        public ICollection<CourseImage> Images { get; set; }


    }
}
