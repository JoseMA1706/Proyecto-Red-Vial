using Microsoft.EntityFrameworkCore;
using Proyecto1.Models;

namespace Proyecto1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Nodo> Nodos { get; set; }
    }
}
