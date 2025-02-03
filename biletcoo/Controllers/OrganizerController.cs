using biletcoo.Data;
using biletcoo.Models;
using biletcoo.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace biletcoo.Controllers
{
    public class OrganizerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<users> userManager;

        public OrganizerController(ApplicationDbContext context, UserManager<users> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ManageEvent()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                // If no user is logged in, redirect or show an error
                return RedirectToAction("Login", "Account");
            }

            // Filter events by this user’s ID
            var userEvents = await _context.Events
                .Where(e => e.OrganizerId == user.Id)
                .ToListAsync();

            return View(userEvents);

            
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);

            if (user != null)
            {

            }

            var events = await _context.Events.ToListAsync();

            var locations = await _context.Events
                .Where(e => e.Status == "Active")
                .Select(e => e.Location)
                .Distinct()
                .OrderBy(location => location)
                .ToListAsync();
            ViewData["Locations"] = locations;

            return View(events);
        }
        public async Task<IActionResult> viewFullDetailsOrganizer(int eventId)
        {
            var eventDetail = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventDetail == null)
            {
                return NotFound();
            }

            return View(eventDetail);
        }

        [HttpGet]
        public IActionResult manageEventDetail(int id)
        {
            var eventItem = _context.Events.FirstOrDefault(e => e.Id == id);
            
            var model = new EventViewModel
            {
                Id = eventItem.Id,
                Name = eventItem.Name,
                Location = eventItem.Location,
                Description = eventItem.Description,
                date = eventItem.date,
                capacity = eventItem.capacity,
                price = eventItem.price,
                category = eventItem.category
            };

            return View(model);
        }

       

        [HttpPost]
        public async Task<IActionResult> UpdateEvent(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.date < DateTime.Today)
                {
                    TempData["color"] = "red";
                    TempData["message"] = "The event date cannot be in the PAST.";
                }
                else
                {
                    var updatedEvent = _context.Events.FirstOrDefault(e => e.Id == model.Id);

                    updatedEvent.Name = model.Name;
                    updatedEvent.Location = model.Location;
                    updatedEvent.Description = model.Description;
                    updatedEvent.date = model.date;
                    updatedEvent.capacity = model.capacity;
                    updatedEvent.price = model.price;
                    updatedEvent.category = model.category;
                    updatedEvent.Status = "Pending";

                    _context.SaveChanges();
                    TempData["color"] = "green";
                    TempData["message"] = "You have successfully approved event!";

                }
                 return RedirectToAction("ManageEvent", "Organizer", new { eventId = model.Id });
            }
            return View(model);
        }


        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                
                var events = new Event
                {
                    Name = model.Name,
                    Location = model.Location,
                    Description = model.Description,
                    date = model.date,
                    capacity = model.capacity,
                    price = model.price,
                    category = model.category,
                    Status = "Pending",
                    OrganizerId = user.Id
                };
                if (model.date < DateTime.Today)
                {
                    TempData["ShowErrorPopup"] = true;
                    TempData["color"] = "red";
                    TempData["redirect"] = "CreateEvent";
                    TempData["PopupErrorMessage"] = "The event date cannot be in the PAST.";
                }
                else
                {
                    _context.Events.Add(events);
                    await _context.SaveChangesAsync();
                    TempData["ShowCreatePopup"] = true;
                    TempData["color"] = "green";
                    TempData["redirect"] = "ManageEvent";
                    TempData["PopupCreateMessage"] = "You have successfully created event!";

                }

            }
            return View(model);
        }



    }
}
