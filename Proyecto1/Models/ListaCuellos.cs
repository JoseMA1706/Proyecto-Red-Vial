namespace Proyecto1.Models
{
    public class ListaCuellos
    {
        private class Elemento
        {
            public Nodo Dato { get; set; }
            public Elemento? Siguiente { get; set; }

            public Elemento(Nodo nodo)
            {
                Dato = nodo;
            }
        }

        private Elemento? cabeza;

        public void Agregar(Nodo nodo)
        {
            var nuevo = new Elemento(nodo);
            if (cabeza == null)
                cabeza = nuevo;
            else
            {
                var actual = cabeza;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }
        }

        public IEnumerable<Nodo> ObtenerTodos()
        {
            var actual = cabeza;
            while (actual != null)
            {
                yield return actual.Dato;
                actual = actual.Siguiente;
            }
        }

        public bool EstaVacia() => cabeza == null;
    }
}

