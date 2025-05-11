using Microsoft.EntityFrameworkCore;
using Proyecto1.Models;

namespace Proyecto1.Data
{
    public class RedVialContext : DbContext
    {
        public RedVialContext(DbContextOptions<RedVialContext> options) : base(options) { }

        public DbSet<Nodo> Nodos { get; set; }
        public DbSet<ReporteRedVial> Reportes { get; set; }
    }
}
