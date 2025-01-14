using ServiceApresVente.Models;

namespace ServiceApresVenteApp.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        
    }
}
