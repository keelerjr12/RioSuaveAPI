using Microsoft.EntityFrameworkCore;
using RioSuaveLib.Events;

namespace RioSuaveLib
{
    public class RioSuaveContext : DbContext
    {
        public RioSuaveContext(DbContextOptions<RioSuaveContext> options) : base(options) {  }
        public DbSet<User>? Users { get; set; }
        public DbSet<Event>? Events { get; set; }
    }
}
