using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using biletcoo.Models;
using Microsoft.AspNetCore.Identity;

namespace biletcoo.Data
{
    
    public class ApplicationDbContext : IdentityDbContext<users>
    {


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Event> Events { get; set; } = default!;
        public DbSet<UserFavorites> UserFavorites { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
