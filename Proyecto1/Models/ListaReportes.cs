using System.Collections.Generic;
using Proyecto1.Models;

namespace Proyecto1.Models
{
    public class ListaReportes
    {
        public class Elemento
        {
            public ReporteRedVial Valor { get; set; }
            public Elemento? Siguiente { get; set; }

            public Elemento(ReporteRedVial valor)
            {
                Valor = valor;
                Siguiente = null;
            }
        }

        private Elemento? cabeza;

        public void Agregar(ReporteRedVial reporte)
        {
            var nuevo = new Elemento(reporte);
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

        public IEnumerable<ReporteRedVial> ObtenerTodos()
        {
            var actual = cabeza;
            while (actual != null)
            {
                yield return actual.Valor;
                actual = actual.Siguiente;
            }
        }

        public bool EstaVacia() => cabeza == null;
    }
}
