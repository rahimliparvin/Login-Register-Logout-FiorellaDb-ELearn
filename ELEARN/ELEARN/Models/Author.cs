namespace ELEARN.Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int SaleCount { get; set; }
        public  ICollection<Course> Courses { get; set; }

    }
}
