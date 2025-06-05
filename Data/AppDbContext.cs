using Microsoft.EntityFrameworkCore;
using Ventixe.EventService.Models;

namespace Ventixe.EventService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
