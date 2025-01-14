using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ServiceApresVenteApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ServiceApresVente.Models;



namespace ServiceApresVenteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> userManager;


        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager)

        {
            this.userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Debug.WriteLine("________________________");
            //var user = userManager.FindByIdAsync(userManager.GetUserId(User));
            //if (user != null)
            //{
            //    Debug.WriteLine(user.Result.Id);
            //}

            //Debug.WriteLine("________________________");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
