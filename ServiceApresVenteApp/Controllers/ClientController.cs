using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Repositories;
using System.Diagnostics;

namespace ServiceApresVenteApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserRepository userRepository;
        private readonly IArticleRepository articleRepository;

        public ClientController(
            UserManager<User> userManager, IUserRepository userRepository,
            IArticleRepository articleRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.articleRepository = articleRepository;

        }
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
            {
                // Handle scenario where userId is null
                return View();
            }
            
            var user = await userManager.FindByIdAsync(userId);
            if (user != null && user.Articles != null)
            {
                var articles = userRepository.GetUserById(user.Id).Articles.ToList();
                return View(articles);
            }

            return View();
        }
        public async Task<IActionResult> Reclamer(int id)
        {
            return RedirectToAction("CreateWithArticleId", "Reclamations", new {ArticleId = id});
        }
        

    }
}
