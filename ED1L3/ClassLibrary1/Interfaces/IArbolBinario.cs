using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TDA.Interfaces
{
    interface IArbolBinario<T,K>
    {
        void Insertar(Nodo<T,K> _nuevo);

        void Eliminar(K _key);

        Nodo<T,K> ObtenerRaiz();

        void EnOrden(RecorridoDelegate<T,K> _recorrido);

        void PreOrden(RecorridoDelegate<T,K> _recorrido);

        void PostOrden(RecorridoDelegate<T,K> _recorrido);
    }
}
