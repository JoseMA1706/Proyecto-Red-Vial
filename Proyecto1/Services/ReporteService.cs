using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Services
{
    public class ReporteService
    {
        private readonly RedVialContext _context;

        public ReporteService(RedVialContext context)
        {
            _context = context;
        }

        public void GuardarReporte(string nodos, string interseccion, string cuellosBotella)
        {
            var reporte = new ReporteRedVial
            {
                NodosTexto = nodos,
                InterseccionMasCongestionada = interseccion,
                CuellosBotellaTexto = cuellosBotella,
                FechaGeneracion = DateTime.Now
            };

            _context.Reportes.Add(reporte);
            _context.SaveChanges();
        }
        public void EliminarHistorial()
        {
            _context.Reportes.RemoveRange(_context.Reportes);
            _context.SaveChanges();
        }

    }
}
