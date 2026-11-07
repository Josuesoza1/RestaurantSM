using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace SistemaPedidoRestaurante.UIPrincipal
{
    internal sealed class ListaOrdenable<T> : BindingList<T>
    {
        private readonly List<T> elementos;
        private PropertyDescriptor columnaOrdenada;
        private ListSortDirection direccion;

        internal ListaOrdenable(IEnumerable<T> datos) : this(new List<T>(datos))
        {
        }

        private ListaOrdenable(List<T> elementos) : base(elementos)
        {
            this.elementos = elementos;
        }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => columnaOrdenada != null;
        protected override PropertyDescriptor SortPropertyCore => columnaOrdenada;
        protected override ListSortDirection SortDirectionCore => direccion;

        protected override void ApplySortCore(PropertyDescriptor columna, ListSortDirection sentido)
        {
            elementos.Sort((primero, segundo) => Comparar(columna, sentido, primero, segundo));
            columnaOrdenada = columna;
            direccion = sentido;
            ResetBindings();
        }

        private static int Comparar(PropertyDescriptor columna, ListSortDirection sentido, T primero, T segundo)
        {
            object valorPrimero = columna.GetValue(primero);
            object valorSegundo = columna.GetValue(segundo);
            if (sentido == ListSortDirection.Descending)
                return Comparer.Default.Compare(valorSegundo, valorPrimero);

            return Comparer.Default.Compare(valorPrimero, valorSegundo);
        }
    }
}
