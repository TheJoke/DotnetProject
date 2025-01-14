using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Repositories;
using System.Diagnostics;

namespace ServiceApresVenteApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserRepository userRepository;

        public ClientController(
            UserManager<User> userManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;

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
                Debug.WriteLine("-----------");

                var articles = userRepository.GetUserById(user.Id).Articles.ToList();
                return View(articles);
            }

            return View();
        }

    }
}
