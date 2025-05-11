using Proyecto1.Data;
using Proyecto1.Models;

namespace Proyecto1.Services
{
    public class RedVial
    {
        private readonly RedVialContext _context;

        public RedVial(RedVialContext context)
        {
            _context = context;
        }

        public string AgregarNodoCentral(Nodo nodo)
        {
            if (_context.Nodos.Any(n => n.Id == nodo.Id))
                return $"Ya existe una intersección con ID '{nodo.Id}'.";

            _context.Nodos.Add(nodo);
            _context.SaveChanges();
            return $"Nodo '{nodo.Id}' agregado correctamente.";
        }

        public string Conectar(string idOrigen, string direccion, Nodo destino, bool bidireccional = false)
        {
            var origen = _context.Nodos.FirstOrDefault(n => n.Id == idOrigen);
            if (origen == null) return $"No se encontró el nodo con ID '{idOrigen}'.";

            if (_context.Nodos.Any(n => n.Id == destino.Id))
                return $"Ya existe una intersección con ID '{destino.Id}'.";

            
            _context.Nodos.Add(destino);

            
            switch (direccion.ToLower())
            {
                case "norte": origen.IdNorte = destino.Id; break;
                case "sur": origen.IdSur = destino.Id; break;
                case "este": origen.IdEste = destino.Id; break;
                case "oeste": origen.IdOeste = destino.Id; break;
                default: return "Dirección inválida. Usa norte, sur, este u oeste.";
            }

            
            if (bidireccional)
            {
                switch (direccion.ToLower())
                {
                    case "norte": destino.IdSur = origen.Id; break;
                    case "sur": destino.IdNorte = origen.Id; break;
                    case "este": destino.IdOeste = origen.Id; break;
                    case "oeste": destino.IdEste = origen.Id; break;
                }
            }

            _context.SaveChanges();
            return $"Nodo '{destino.Id}' conectado al {direccion} de '{origen.Id}'" +
                   (bidireccional ? " (conexión bidireccional)." : ".");
        }

        public CaminoNodos? EncontrarCamino(string idOrigen, string idDestino)
        {
            var nodos = _context.Nodos.ToList();
            var mapa = nodos.ToDictionary(n => n.Id);

            
            foreach (var nodo in nodos)
            {
                if (!string.IsNullOrEmpty(nodo.IdNorte) && mapa.ContainsKey(nodo.IdNorte))
                    nodo.Norte = mapa[nodo.IdNorte];
                if (!string.IsNullOrEmpty(nodo.IdSur) && mapa.ContainsKey(nodo.IdSur))
                    nodo.Sur = mapa[nodo.IdSur];
                if (!string.IsNullOrEmpty(nodo.IdEste) && mapa.ContainsKey(nodo.IdEste))
                    nodo.Este = mapa[nodo.IdEste];
                if (!string.IsNullOrEmpty(nodo.IdOeste) && mapa.ContainsKey(nodo.IdOeste))
                    nodo.Oeste = mapa[nodo.IdOeste];
            }

            if (!mapa.ContainsKey(idOrigen) || !mapa.ContainsKey(idDestino))
                return null;

            var origen = mapa[idOrigen];
            var destino = mapa[idDestino];
            var visitados = new HashSet<string>();
            var camino = new CaminoNodos();

            if (Buscar(origen, destino, visitados, camino))
                return camino;

            return null;
        }

        private bool Buscar(Nodo actual, Nodo destino, HashSet<string> visitados, CaminoNodos camino)
        {
            if (visitados.Contains(actual.Id)) return false;

            visitados.Add(actual.Id);
            camino.AgregarPaso(actual);

            if (actual.Id == destino.Id)
                return true;

            if (actual.Norte != null && Buscar(actual.Norte, destino, visitados, camino)) return true;
            if (actual.Sur != null && Buscar(actual.Sur, destino, visitados, camino)) return true;
            if (actual.Este != null && Buscar(actual.Este, destino, visitados, camino)) return true;
            if (actual.Oeste != null && Buscar(actual.Oeste, destino, visitados, camino)) return true;

            camino.EliminarUltimoPaso();
            return false;
        }
    }
}



