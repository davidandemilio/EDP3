﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TDA.Interfaces
{
    interface IArbolBinario<T>
    {
        void Insertar(Nodo<T> _nuevo);

        void Eliminar(T _key);

        Nodo<T> ObtenerRaiz();

        void EnOrden(RecorridoDelegate<T> _recorrido);

        void PreOrden(RecorridoDelegate<T> _recorrido);

        void PostOrden(RecorridoDelegate<T> _recorrido);
    }
}
