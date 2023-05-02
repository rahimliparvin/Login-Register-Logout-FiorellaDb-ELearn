using ELEARN.Models;

namespace ELEARN.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAll();
    }
}
