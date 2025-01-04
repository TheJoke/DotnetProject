using ServiceApresVente.Models;

namespace ServiceApresVenteApp.Repositories
{
    public interface IArticleRepository
    {
        IList<Article> GetAll();
        Article GetById(int id);
        void Add(Article s);
        void Edit(Article s);
        void Delete(Article s);

    }
}
