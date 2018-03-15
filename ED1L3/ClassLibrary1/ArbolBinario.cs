using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TDA.Interfaces;
namespace TDA
{
  

    public class Nodo<T> : IComparable<T>
    {
        public T valor { get; set; }

        public Nodo<T> izquierdo { get; set; }

        public Nodo<T> derecho { get; set; }

        public Nodo<T> padre { get; set; }

        public ComparadorNodosDelegate<T> comparador;

        public Nodo(T _value, ComparadorNodosDelegate<T> _comparador)
        {
            this.valor = _value;
            this.izquierdo = null;
            this.derecho = null;
            comparador = _comparador;
        }

        public int CompareTo(T _other)
        {
            return comparador(this.valor, _other);
        }
    }

    public class ArbolBinarioBusqueda<T>
    {
        public Nodo<T> Raiz { get; set; }

        public ArbolBinarioBusqueda()
        {
            Raiz = null;
        }

        public void Eliminar(T _key)
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

        public void EliminacionInterna(Nodo<T> _padre, Nodo<T> _actual, T _key, string hijo)
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
                    }
                    else
                    {
                        _padre.izquierdo = _actual.izquierdo;
                    }
                }
                else if (_actual.derecho != null && _actual.izquierdo == null)
                {
                    if (hijo == "derecho")
                    {
                        _padre.derecho = _actual.derecho;
                    }
                    else
                    {
                        _padre.izquierdo = _actual.derecho;
                    }
                }
                //Borrar un nodo con dos subarboles hijos
                if (_actual.derecho != null && _actual.izquierdo != null)
                {
                    Nodo<T> nodo_Temp = _actual.derecho;
                    while (nodo_Temp.izquierdo != null)
                    {
                        nodo_Temp = nodo_Temp.izquierdo;
                    }
                    Eliminar(nodo_Temp.valor);
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

        private void equilibrar() {

        }

        public void EnOrden(RecorridoDelegate<T> _recorrido)
        {
            RecorridoEnOrdenInterno(_recorrido, Raiz);
        }

        public void Insertar(Nodo<T> _nuevo)
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

        public Nodo<T> ObtenerRaiz()
        {
            return Raiz;
        }

        public void PostOrden(RecorridoDelegate<T> _recorrido)
        {
            RecorridoPostOrdenInterno(_recorrido, Raiz);
        }

        public void PreOrden(RecorridoDelegate<T> _recorrido)
        {
            RecorridoPreOrdenInterno(_recorrido, Raiz);
        }

        private void InsercionInterna(Nodo<T> _actual, Nodo<T> _nuevo)
        {
            if (_actual.CompareTo(_nuevo.valor) < 0)
            {
                if (_actual.derecho == null)
                {
                    _actual.derecho = _nuevo;
                    
                }
                else
                {
                    InsercionInterna(_actual.derecho, _nuevo);
                }
            }
            else if (_actual.CompareTo(_nuevo.valor) > 0)
            {
                if (_actual.izquierdo == null)
                {
                    _actual.izquierdo = _nuevo;
                }
                else
                {
                    InsercionInterna(_actual.izquierdo, _nuevo);
                }
            }
        } //Fin de inserción interna.

        private void RecorridoEnOrdenInterno(RecorridoDelegate<T> _recorrido, Nodo<T> _actual)
        {
            if (_actual != null)
            {
                RecorridoEnOrdenInterno(_recorrido, _actual.izquierdo);

                _recorrido(_actual);

                RecorridoEnOrdenInterno(_recorrido, _actual.derecho);
            }
        }

        private void RecorridoPostOrdenInterno(RecorridoDelegate<T> _recorrido, Nodo<T> _actual)
        {
            if (_actual != null)
            {
                RecorridoEnOrdenInterno(_recorrido, _actual.izquierdo);

                RecorridoEnOrdenInterno(_recorrido, _actual.derecho);

                _recorrido(_actual);
            }
        }

        private void RecorridoPreOrdenInterno(RecorridoDelegate<T> _recorrido, Nodo<T> _actual)
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
    

