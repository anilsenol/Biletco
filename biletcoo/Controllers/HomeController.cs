using System.Diagnostics;
using biletcoo.Data;
using biletcoo.Models;
using biletcoo.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace biletcoo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<users> signInManager;
        private readonly UserManager<users> userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, SignInManager<users> signInManager, UserManager<users> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            _context = context;

        }
        public IActionResult Index()
        { 
            return View(); 
        }


        public async Task<IActionResult> Main()
        {
            var user = await userManager.GetUserAsync(User);
            var favoriteEventIds = new List<int>();

            if (user != null)
            {
                favoriteEventIds = await _context.UserFavorites
                    .Where(f => f.UserId == user.Id)
                    .Select(f => f.EventId)
                    .ToListAsync();
            }

            var events = await _context.Events.ToListAsync();

            ViewData["Favorites"] = favoriteEventIds; // Pass favorite event IDs to the view

            var locations = await _context.Events
                .Where(e => e.Status == "Active")
                .Select(e => e.Location)
                .Distinct()
                .OrderBy(location => location)
                .ToListAsync();
            ViewData["Locations"] = locations;


            return View(events);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
