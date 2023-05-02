using ELEARN.Data;
using ELEARN.Models;
using ELEARN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ELEARN.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Author>> GetAll() => await _context.Authors.ToListAsync();
    }

}
