using ServiceApresVente.Models;

namespace ServiceApresVenteApp.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        readonly ApplicationDbContext context;
        public ArticleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IList<Article> GetAll()
        {
            
            return context.Articles.OrderBy(s => s.Nom).ToList();
        }
        public Article GetById(int id)
        {
            return context.Articles.Find(id);
        }
        public void Add(Article s)
        {
            context.Articles.Add(s);
            context.SaveChanges();
        }
        public void Edit(Article s)
        {
            Article s1 = context.Articles.Find(s.Id);
            if (s1 != null)
            {
                s1.Nom = s.Nom;
                s1.Reference = s.Reference;
                s1.DureeGarantie = s.DureeGarantie;
                context.SaveChanges();
            }
        }
        public void Delete(Article s)
        {
            Article s1 = context.Articles.Find(s.Id);
            if (s1 != null)
            {
                context.Articles.Remove(s1);
                context.SaveChanges();
            }
        }

        public void Update(Article s)
        {
            throw new NotImplementedException();
        }

        public List<Article> GetByUserId(int userId)
        {
            var articles = context.Articles.Where(a=>a.UserId == userId).ToList();
            return articles;
        }


    }
}
