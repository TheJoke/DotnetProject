using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Repositories;
using ServiceApresVenteApp.ViewModels;

namespace ServiceApresVenteApp.Controllers
{
    public class ResponsibleController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IArticleRepository articleRepository;
        private readonly IUserRepository userRepository;

        public ResponsibleController( 
            UserManager<User> userManager, 
            IUserRepository userRepository,
            IArticleRepository articleRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.articleRepository = articleRepository;
        }
        public IActionResult Index()
        {
            return View(articleRepository.GetAll());
        }
        public IActionResult CreateArticle()
        {
            ViewData["UserId"] = new SelectList(userRepository.GetAll(), "Id", "Id");
            return View();
        }

        
    }
}
