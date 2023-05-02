using ELEARN.Models;

namespace ELEARN.Services.Interfaces
{
    public interface ICourseService
    {

        Task<IEnumerable<Course>> GetAll();

        Task<Course> GetFullDataById (int id);

        Task<Course> GetById(int id);

    }
}
