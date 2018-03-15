using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TDA.Interfaces;
namespace TDA
{
  

    public class Nodo<T,K> : IComparable<K>
    {
        public T valor { get; set; }

        public K llave { get; set; }

        public Nodo<T,K> izquierdo { get; set; }

        public Nodo<T, K> derecho { get; set; }

        public Nodo<T, K> padre { get; set; }

        public ComparadorNodosDelegate<K> comparador;

        public Nodo(T _value, ComparadorNodosDelegate<K> _comparador)
        {
            this.valor = _value;
            this.izquierdo = null;
            this.derecho = null;
            comparador = _comparador;
        }

        public int CompareTo(K _other)
        {
            return comparador(this.llave, _other);
        }
    }

    public class ArbolBinarioBusqueda<T,K>
    {
        public Nodo<T,K> Raiz { get; set; }

        public ArbolBinarioBusqueda()
        {
            Raiz = null;
        }

        public void Eliminar(K _key)
        {
            if (Raiz == null)
            {
                throw new NullReferenceException();
            }
            else if (Raiz.CompareTo(_key) == 0)
            {
                Raiz = null;
            }
            else
            {
                if (Raiz.CompareTo(_key) < 0)
                {
                    EliminacionInterna(Raiz, Raiz.derecho, _key, "derecho");
                }
                else if (Raiz.CompareTo(_key) > 0)
                {
                    EliminacionInterna(Raiz, Raiz.izquierdo, _key, "izquierdo");
                }
            }
        }

        public void EliminacionInterna(Nodo<T,K> _padre, Nodo<T,K> _actual, K _key, string hijo)
        {
            if (_actual.CompareTo(_key) == 0)
            {
                // Borrar un nodo sin hijos
                if (_actual.derecho == null && _actual.izquierdo == null)
                {
                    if (hijo == "derecho")
                    {
                        _padre.derecho = null;
                    }
                    else
                    {
                        _padre.izquierdo = null;
                    }
                }
                //Borrar un nodo con un subarbol hijo
                if (_actual.derecho == null && _actual.izquierdo != null)
                {
                    if (hijo == "derecho")
                    {
                        _padre.derecho = _actual.izquierdo;
                        _actual.izquierdo.padre = _padre;

                    }
                    else
                    {
                        _padre.izquierdo = _actual.izquierdo;
                        _actual.izquierdo.padre = _padre;
                    }
                }
                else if (_actual.derecho != null && _actual.izquierdo == null)
                {
                    if (hijo == "derecho")
                    {
                        _padre.derecho = _actual.derecho;
                        _actual.derecho.padre = _padre;
                    }
                    else
                    {
                        _padre.izquierdo = _actual.derecho;
                        _actual.derecho.padre = _padre;
                    }
                }
                //Borrar un nodo con dos subarboles hijos
                if (_actual.derecho != null && _actual.izquierdo != null)
                {
                    Nodo<T,K> nodo_Temp = _actual.derecho;
                    while (nodo_Temp.izquierdo != null)
                    {
                        nodo_Temp = nodo_Temp.izquierdo;
                    }
                    Eliminar(nodo_Temp.llave);
                    _actual.valor = nodo_Temp.valor;
                }
            }
            else if (_actual.CompareTo(_key) < 0)
            {
                EliminacionInterna(_actual, _actual.derecho, _key, "derecho");
            }
            else if (_actual.CompareTo(_key) > 0)
            {
                EliminacionInterna(_actual, _actual.izquierdo, _key, "izquierdo");
            }
        }

        private void equilibrar(Nodo<T,K> ACT) {

        }

        public void EnOrden(RecorridoDelegate<T,K> _recorrido)
        {
            RecorridoEnOrdenInterno(_recorrido, Raiz);
        }

        public void Insertar(Nodo<T, K> _nuevo)
        {
            if (Raiz == null)
            {
                Raiz = _nuevo;
            }
            else
            {
                InsercionInterna(Raiz, _nuevo);
            }
        }

        public Nodo<T, K> ObtenerRaiz()
        {
            return Raiz;
        }

        public void PostOrden(RecorridoDelegate<T, K> _recorrido)
        {
            RecorridoPostOrdenInterno(_recorrido, Raiz);
        }

        public void PreOrden(RecorridoDelegate<T, K> _recorrido)
        {
            RecorridoPreOrdenInterno(_recorrido, Raiz);
        }

        private void InsercionInterna(Nodo<T, K> _actual, Nodo<T, K> _nuevo)
        {
            if (_actual.CompareTo(_nuevo.llave) < 0)
            {
                if (_actual.derecho == null)
                {
                    _actual.derecho = _nuevo;
                    _nuevo.padre = _actual;
                    
                }
                else
                {
                    InsercionInterna(_actual.derecho, _nuevo);
                }
            }
            else if (_actual.CompareTo(_nuevo.llave) > 0)
            {
                if (_actual.izquierdo == null)
                {
                    _actual.izquierdo = _nuevo;
                    _nuevo.padre = _actual;
                }
                else
                {
                    InsercionInterna(_actual.izquierdo, _nuevo);
                }
            }
        } //Fin de inserción interna.

        private void RecorridoEnOrdenInterno(RecorridoDelegate<T, K> _recorrido, Nodo<T, K> _actual)
        {
            if (_actual != null)
            {
                RecorridoEnOrdenInterno(_recorrido, _actual.izquierdo);

                _recorrido(_actual);

                RecorridoEnOrdenInterno(_recorrido, _actual.derecho);
            }
        }

        private void RecorridoPostOrdenInterno(RecorridoDelegate<T, K> _recorrido, Nodo<T, K> _actual)
        {
            if (_actual != null)
            {
                RecorridoEnOrdenInterno(_recorrido, _actual.izquierdo);

                RecorridoEnOrdenInterno(_recorrido, _actual.derecho);

                _recorrido(_actual);
            }
        }

        private void RecorridoPreOrdenInterno(RecorridoDelegate<T, K> _recorrido, Nodo<T, K> _actual)
        {
            if (_actual != null)
            {
                _recorrido(_actual);

                RecorridoEnOrdenInterno(_recorrido, _actual.izquierdo);

                RecorridoEnOrdenInterno(_recorrido, _actual.derecho);
            }
        }
    }
}
    

