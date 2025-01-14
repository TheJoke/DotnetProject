using ServiceApresVente.Models;

namespace ServiceApresVenteApp.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetUserById(int id);
        
    }
}
