using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA.Interfaces
{
   
        public delegate int ComparadorNodosDelegate<K>(K _actual, K _nuevo);

        public delegate void RecorridoDelegate<T,K>(Nodo<T,K> _acutal);

    public delegate string Convertirastring<K>(K llave);

}
