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
            throw new NotImplementedException();
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
    

