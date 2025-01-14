using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;

namespace ServiceApresVenteApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public User GetUserById(int id)
        {
            return _context.Users.Include(u => u.Articles).FirstOrDefault(x => x.Id == id);
        }
    }
}
