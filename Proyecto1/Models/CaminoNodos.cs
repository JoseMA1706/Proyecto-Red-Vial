namespace Proyecto1.Models
{
    public class CaminoNodos
    {
        public class Paso
        {
            public Nodo Nodo { get; set; }
            public Paso? Siguiente { get; set; }

            public Paso(Nodo nodo)
            {
                Nodo = nodo;
                Siguiente = null;
            }
        }

        private Paso? inicio;

        public CaminoNodos()
        {
            inicio = null;
        }

        public void AgregarPaso(Nodo nodo)
        {
            var nuevo = new Paso(nodo);
            if (inicio == null)
                inicio = nuevo;
            else
            {
                Paso actual = inicio;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }
        }

        public void EliminarUltimoPaso()
        {
            if (inicio == null) return;

            if (inicio.Siguiente == null)
            {
                inicio = null;
                return;
            }

            Paso actual = inicio;
            while (actual.Siguiente?.Siguiente != null)
                actual = actual.Siguiente;

            actual.Siguiente = null;
        }

        public IEnumerable<Nodo> ObtenerRuta()
        {
            Paso? actual = inicio;
            while (actual != null)
            {
                yield return actual.Nodo;
                actual = actual.Siguiente;
            }
        }
        public Nodo? ObtenerUltimo()
        {
            Paso? actual = inicio;
            if (actual == null) return null;

            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }

            return actual.Nodo;
        }


        public bool EstaVacio()
        {
            return inicio == null;
        }

        public double TiempoTotal()
        {
            double total = 0;
            foreach (var nodo in ObtenerRuta())
            {
                total += nodo.TiempoPromedioCruce;
            }
            return total;
        }
    }
}
