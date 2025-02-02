using biletcoo.Data;
using biletcoo.Models;
using biletcoo.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Reflection;
using System.Security.Policy;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace biletcoo.Controllers
{
    public class AdminController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<users> signInManager;
        private readonly UserManager<users> userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(ILogger<HomeController> logger, SignInManager<users> signInManager, UserManager<users> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            _context = context;

        }
        public async Task<IActionResult> ManageEventAdmin(int eventId)
        {
            var eventDetail = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventDetail == null)
            {
                return NotFound();
            }

            return View(eventDetail);
        }
        public async Task<IActionResult> AdminFullEvents()
        {
            var user = await userManager.GetUserAsync(User);

            if (user != null)
            {
            }

            var events = await _context.Events.ToListAsync();

            var locations = await _context.Events
                .Select(e => e.Location)
                .Distinct()
                .OrderBy(location => location)
                .ToListAsync();
            ViewData["Locations"] = locations;

            return View(events);
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            if (user != null)
            {
            }

            var events = await _context.Events.ToListAsync();

            var locations = await _context.Events
                .Where(e => e.Status == "Pending")
                .Select(e => e.Location)
                .Distinct()
                .OrderBy(location => location)
                .ToListAsync();
            ViewData["Locations"] = locations;

            return View(events);
        }

        

        public async Task<IActionResult> approve(int eventId)
        {
            var eventDetail = await _context.Events
                 .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventDetail == null)
            {
                return NotFound();
            }
            else
            {
                eventDetail.Status = "Active";
                _context.SaveChanges();
                TempData["color"] = "green";
                TempData["message"] = "You have successfully approved event!";
            }

            return RedirectToAction("ManageEventAdmin", "Admin", new { eventId = eventId });
        }
        public async Task<IActionResult> reject(int eventId)
        {
            var eventDetail = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventDetail == null)
            {
                return NotFound();
            }
            else
            {
                eventDetail.Status = "Declined";
                _context.SaveChanges();
                TempData["color"] = "red";
                TempData["message"] = "You have rejected the event";
            }

            return RedirectToAction("ManageEventAdmin", "Admin", new { eventId = eventId });

        }





    }
}





