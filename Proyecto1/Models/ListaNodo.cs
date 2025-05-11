using System.Collections.Generic;

namespace Proyecto1.Models
{
    public class ListaNodos
    {
        public class Elemento
        {
            public Nodo Valor { get; set; }
            public Elemento? Siguiente { get; set; }

            public Elemento(Nodo valor)
            {
                Valor = valor;
                Siguiente = null;
            }
        }

        private Elemento? cabeza;

        public ListaNodos()
        {
            cabeza = null;
        }

        public void Agregar(Nodo nodo)
        {
            var nuevo = new Elemento(nodo);
            if (cabeza == null)
                cabeza = nuevo;
            else
            {
                Elemento actual = cabeza;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }
        }

        public IEnumerable<Nodo> ObtenerTodos()
        {
            Elemento? actual = cabeza;
            while (actual != null)
            {
                yield return actual.Valor;
                actual = actual.Siguiente;
            }
        }

        public bool EstaVacia()
        {
            return cabeza == null;
        }

        public Nodo? BuscarPorId(string? id)
        {
            if (id == null) return null;
            var actual = cabeza;
            while (actual != null)
            {
                if (actual.Valor.Id == id)
                    return actual.Valor;
                actual = actual.Siguiente;
            }
            return null;
        }
    }
}

