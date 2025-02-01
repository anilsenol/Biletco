using System.Net.Sockets;
using biletcoo.Data;
using biletcoo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace biletcoo.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<users> signInManager;
        private readonly UserManager<users> userManager;
        private readonly ApplicationDbContext _context;

        public UserController(SignInManager<users> signInManager, UserManager<users> userManager, ApplicationDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _context = context;

        }

        public async Task<IActionResult> myTickets()
        {
            var user = await userManager.GetUserAsync(User);


            if (user == null)
                return NotFound();

            var tickets = await _context.Tickets
            .Where(t => t.userId == user.Id)
            .Include(t => t.MyEvent)  
            .ToListAsync();

            foreach (var ticket in tickets)
            {
                
                if (ticket.MyEvent != null && ticket.MyEvent.date < DateTime.Now)
                {
                    ticket.MyEvent.Status = "Expired";
                    await _context.SaveChangesAsync();
                }
            }
            
            
            return View(tickets);
        }


        

        [HttpPost]
        public async Task<IActionResult> getYourTicket(int eventId, int ticketNumber) 
        {
            var user = await userManager.GetUserAsync(User);
            var eventObject = await _context.Events
                .Where(e => e.Id ==eventId)
                .FirstOrDefaultAsync();


            if (user == null)
                return NotFound();

            if (ticketNumber > eventObject.capacity)
            {
                TempData["color"] = "red";
                TempData["message"] = "You cannot buy more tickets than the remaining capacity of the event.";
                return View("EventDetails", eventObject); // Stay on the event details page
            }
            else
            {
                eventObject.capacity -= ticketNumber;
            }

            int ticketno;
            bool isDuplicate;

            do
            {
                // Generate a random ticket number
                ticketno = new Random().Next(10000, 99999);

                // Check if the ticket number already exists in the database
                isDuplicate = await _context.Tickets.AnyAsync(t => t.ticketNumber == ticketno);

            } while (isDuplicate);



            var ticket = new Ticket { ticketNumber = ticketno, userId = user.Id, eventId = eventId };

            _context.Add(ticket);
            TempData["color"] = "green";
            TempData["message"] = "Your ticket has been successfully purchased! Your ticket number is " + ticketno;
            await _context.SaveChangesAsync();

            return RedirectToAction("myTickets", "User");

        }


        public async Task<IActionResult> favouriteEvent()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();


            var favoriteEventIds = await _context.UserFavorites
                .Where(f => f.UserId == user.Id)
                .Select(f => f.EventId)
                .ToListAsync();

            var favoriteEvents = await _context.Events
                .Where(e => favoriteEventIds.Contains(e.Id))
                .ToListAsync();

            return View(favoriteEvents);
        }


        public async Task<IActionResult> ToggleFavorite(int eventId)
        {
            var user = await userManager.GetUserAsync(User);

            if (user != null)
            {
                var existingFavorite = await _context.UserFavorites
                    .FirstOrDefaultAsync(f => f.UserId == user.Id && f.EventId == eventId);

                if (existingFavorite == null)
                {
                    var newFavorite = new UserFavorites { UserId = user.Id, EventId = eventId };
                    _context.UserFavorites.Add(newFavorite);
                }
                else
                {
                    _context.UserFavorites.Remove(existingFavorite);
                }

                await _context.SaveChangesAsync();
                
            }

            return RedirectToAction("Main", "Home");
        }
        public async Task<IActionResult> EventDetails(int eventId)
        {
            var eventDetail = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventDetail == null)
            {
                return NotFound();
            }

            return View(eventDetail);
        }


    }
}
