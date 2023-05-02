using System.ComponentModel.DataAnnotations.Schema;

namespace ELEARN.Models
{
    public class Course : BaseEntity    
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<CourseImage> CourseImages { get; set; } 

    }
}
