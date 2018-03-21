using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TDA.Interfaces;
using System.IO;

namespace TDA
{
    public class Nodo<T, K> : IComparable<K>
    {
        public T valor { get; set; }

        public K llave { get; set; }

        public Nodo<T, K> izquierdo { get; set; }

        public Nodo<T, K> derecho { get; set; }

        public Nodo<T, K> padre { get; set; }

        public ComparadorNodosDelegate<K> comparador;

        public int f_e;

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

   

    public class ArbolBinarioBusqueda<T, K>
    {

       public Log<K> Nuevolog = new Log<K>();

        
        StreamWriter w = File.AppendText(@"C:\Users\Public\bit.txt");
         
      

        public Nodo<T, K> Raiz { get; set; }

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
                if (Raiz.derecho == null)
                {
                    Raiz = Raiz.izquierdo;
                }
                else if (Raiz.izquierdo == null)
                {
                    Raiz = Raiz.derecho;
                }
                else
                {
                    Nodo<T, K> nodo_Temp = Raiz.derecho;
                    Nodo<T, K> padre_nodo_Temp = null;
                    int cont = 0;
                    while (nodo_Temp.izquierdo != null)
                    {
                        nodo_Temp = nodo_Temp.izquierdo;
                        cont++;
                    }
                    padre_nodo_Temp = nodo_Temp.padre;
                    Eliminar(nodo_Temp.llave);
                    Raiz.valor = nodo_Temp.valor;
                    Raiz.llave = nodo_Temp.llave;

                }

                Nuevolog.Eliminacion(w,Raiz.llave);
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

    

        public void EliminacionInterna(Nodo<T, K> _padre, Nodo<T, K> _actual, K _key, string hijo)
        {
            if (_actual.CompareTo(_key) == 0)
            {
                // Borrar un nodo sin hijos
                if (_actual.derecho == null && _actual.izquierdo == null)
                {
                    if (hijo == "derecho")
                    {
                        _padre.derecho = null;
                        actualizar_factores(_padre, hijo, false);
                  
                    }
                    else
                    {
                        _padre.izquierdo = null;
                        actualizar_factores(_padre, hijo, false);
                       
                    }
                   
                }
                //Borrar un nodo con un subarbol hijo
                if (_actual.derecho == null && _actual.izquierdo != null)
                {
                    if (hijo == "derecho")
                    {
                        _padre.derecho = _actual.izquierdo;
                        _actual.izquierdo.padre = _padre;
                        actualizar_factores(_padre, hijo, false);
                       

                    }
                    else
                    {
                        _padre.izquierdo = _actual.izquierdo;
                        _actual.izquierdo.padre = _padre;
                        actualizar_factores(_padre, hijo, false);
                     
                    }
                }
                else if (_actual.derecho != null && _actual.izquierdo == null)
                {
                    if (hijo == "derecho")
                    {
                        _padre.derecho = _actual.derecho;
                        _actual.derecho.padre = _padre;
                        actualizar_factores(_padre, hijo, false);
                      
                    }
                    else
                    {
                        _padre.izquierdo = _actual.derecho;
                        _actual.derecho.padre = _padre;
                        actualizar_factores(_padre, hijo, false);
                       
                    }
                }
                //Borrar un nodo con dos subarboles hijos
                if (_actual.derecho != null && _actual.izquierdo != null)
                {
                    Nodo<T, K> nodo_Temp = _actual.derecho;
                    Nodo<T, K> padre_nodo_Temp = null;
                    int cont = 0;
                    while (nodo_Temp.izquierdo != null)
                    {
                        nodo_Temp = nodo_Temp.izquierdo;
                        cont++;
                    }
                    padre_nodo_Temp = nodo_Temp.padre;
                    Eliminar(nodo_Temp.llave);
                    _actual.valor = nodo_Temp.valor;
                    _actual.llave = nodo_Temp.llave;





                    Nuevolog.Eliminacion(w, _actual.llave);
                }
               
            } else if (_actual.CompareTo(_key) < 0)
                {
                    EliminacionInterna(_actual, _actual.derecho, _key, "derecho");
                }
                else if (_actual.CompareTo(_key) > 0)
                {
                    EliminacionInterna(_actual, _actual.izquierdo, _key, "izquierdo");
                }
            
        }

     

        private void actualizar_factores(Nodo<T, K> nodo_inicio, string hijo, bool nuevo)
        {
            bool salir = false;
            bool rotacion = false;
            while ((nodo_inicio != null) && !salir) {

                if (nuevo)
                {
                    if (hijo == "izquierdo")
                        nodo_inicio.f_e--;
                    else
                        nodo_inicio.f_e++;
                }
                else
                {
                    if (hijo == "izquierdo")
                        nodo_inicio.f_e++;
                    else
                        nodo_inicio.f_e--;
                }
                if (nodo_inicio.f_e == 0) { 
                    salir = true;
            }else if (nodo_inicio.f_e == 2)
                { 
                    if (nodo_inicio.derecho.f_e == -1)
                    {
                        RotacionDobleIzquierda(nodo_inicio); 
                        rotacion = true;
                    }
                    else
                    {
                        RotacionSimpleIzquierda(nodo_inicio);
                        rotacion = true;
                    }
                    salir = true;
                }
                else if (nodo_inicio.f_e == -2)
                {
                    if (nodo_inicio.izquierdo.f_e == 1)
                    {
                        Rotacion_Doble_Derecha(nodo_inicio);
                        rotacion = true;
                    }
                    else
                    {
                        RotacionSimpleDer(nodo_inicio);
                        rotacion = true;
                    }
                    salir = true;
                }

                if ((rotacion) && (nodo_inicio.padre != null) && (!nuevo))
                {
                    nodo_inicio = nodo_inicio.padre;
                }

                if (nodo_inicio.padre != null)
                {              
                    if (nodo_inicio.padre.derecho == nodo_inicio)
                        hijo = "derecho";
                    else
                        hijo = "izquierdo";

                    if ((!nuevo) &&(nodo_inicio.f_e == 0))
                        salir = false;
                    
                }

                nodo_inicio = nodo_inicio.padre;
            }

      



        }


        private void RotacionSimpleIzquierda(Nodo<T, K> nodo)
        {

            Nodo<T, K> Padre = nodo.padre;
            Nodo<T, K> Nodo_des = nodo;
            Nodo<T, K> Nodo_des_der = Nodo_des.derecho;
            Nodo<T, K> izq_Nodo_des_der = Nodo_des_der.izquierdo;

            if (Padre != null)
            {
                if (Padre.derecho == Nodo_des)
                    Padre.derecho = Nodo_des_der;
                else
                    Padre.izquierdo = Nodo_des_der;
            }
            else
            {
                Raiz = Nodo_des_der as Nodo<T, K>;
                Raiz.padre = null;


            }


            Nodo_des.derecho = izq_Nodo_des_der;
            Nodo_des_der.izquierdo = Nodo_des;


            Nodo_des.padre = Nodo_des_der;
            if (izq_Nodo_des_der != null)
                izq_Nodo_des_der.padre = Nodo_des;
            Nodo_des_der.padre = Padre;


            if (Nodo_des_der.f_e == 0)
            {
                Nodo_des.f_e = 1;
                Nodo_des_der.f_e = -1;
            }
            else
            {
                Nodo_des.f_e = 0;
                Nodo_des_der.f_e = 0;
            }
            Nuevolog.ROTSIMPIZQ(w,nodo.llave);
        }


        private void RotacionDobleIzquierda(Nodo<T, K> nodo)
        {
            
            Nodo<T, K> Padre = nodo.padre;
            Nodo<T, K> Nodo_des = nodo;
            Nodo<T, K> Nodo_des_der = nodo.derecho;
            Nodo<T, K> Iz_Nodo_des_de = Nodo_des_der.izquierdo;
            Nodo<T, K> Iz_Nodo_des_de_iz = Iz_Nodo_des_de.izquierdo;
            Nodo<T, K> Iz_Nodo_des_de_der = Iz_Nodo_des_de.derecho;

        
            if (Padre != null)
            {
                if (Padre.derecho == Nodo_des)
                    Padre.derecho = Iz_Nodo_des_de;
                else
                    Padre.izquierdo = Iz_Nodo_des_de;
            }
            else
            {
                Raiz = Iz_Nodo_des_de as Nodo<T, K>;
                Raiz.padre = null; 
            }

        
            Nodo_des.derecho = Iz_Nodo_des_de_iz;
            Nodo_des_der.izquierdo = Iz_Nodo_des_de_der;
            Iz_Nodo_des_de.izquierdo = Nodo_des;
            Iz_Nodo_des_de.derecho = Nodo_des_der;

            Iz_Nodo_des_de.padre = Padre;
            Nodo_des.padre = Nodo_des_der.padre = Iz_Nodo_des_de;
            if (Iz_Nodo_des_de_iz != null)
                Iz_Nodo_des_de_iz.padre = Nodo_des;
            if (Iz_Nodo_des_de_der != null)
                Iz_Nodo_des_de_der.padre = Nodo_des_der;

            
            switch (Iz_Nodo_des_de.f_e)
            {
                case -1:
                    {
                        Nodo_des.f_e = 0;
                        Nodo_des_der.f_e = 1;
                    }
                    break;

                case 0:
                    {
                        Nodo_des.f_e = 0;
                        Nodo_des_der.f_e = 0;
                    }
                    break;

                case 1:
                    {
                        Nodo_des.f_e = -1;
                        Nodo_des_der.f_e = 0;
                    }
                    break;
            }
            Iz_Nodo_des_de.f_e = 0;
            Nuevolog.ROTDOBIZQ(w, nodo.llave);
        }

       
        private void RotacionSimpleDer(Nodo<T, K> nodo)
        {
          
            Nodo<T, K> Padre = nodo.padre; 
            Nodo<T, K> Nododes = nodo; 
            Nodo<T, K> Nodo_des_iz = Nododes.izquierdo;
            Nodo<T, K> Nodo_des_iz_der = Nodo_des_iz.derecho; 

            if (Padre != null)
            {
                if (Padre.derecho == Nododes)
                    Padre.derecho = Nodo_des_iz;
                else
                    Padre.izquierdo = Nodo_des_iz;
            }
            else
            {
                Raiz = Nodo_des_iz as Nodo<T, K>;
                Raiz.padre = null;
            }

    
            Nododes.izquierdo = Nodo_des_iz_der;
            Nodo_des_iz.derecho = Nododes;

          
            Nododes.padre = Nodo_des_iz;
            if (Nodo_des_iz_der != null)
                Nodo_des_iz_der.padre = Nododes;
            Nodo_des_iz.padre = Padre;


            if (Nodo_des_iz.f_e == 0)
            {
                Nododes.f_e = -1;
                Nodo_des_iz.f_e = 1;

            }
            else
            {
                Nododes.f_e = 0;
                Nodo_des_iz.f_e = 0;
            }
            Nuevolog.ROTSIMPDER(w, nodo.llave);

        }

        private void Rotacion_Doble_Derecha(Nodo<T, K> nodo)
        {
           
            Nodo<T, K> Padre = nodo.padre;
            Nodo<T, K> Nodo_des = nodo;
            Nodo<T, K> Nodo_des_iz = Nodo_des.izquierdo;
            Nodo<T, K> Nodo_des_iz_der = Nodo_des_iz.derecho;
            Nodo<T, K> Nodo_des_iz_der_iz = Nodo_des_iz_der.izquierdo;
            Nodo<T, K> Nodo_des_iz_der_der = Nodo_des_iz_der.derecho;

       
            if (Padre != null)
            {
                if (Padre.derecho == Nodo_des)
                    Padre.derecho = Nodo_des_iz_der;
                else
                    Padre.izquierdo = Nodo_des_iz_der;
            }
            else
            {
                Raiz = Nodo_des_iz_der as Nodo<T, K>;
                Raiz.padre = null; 
            }

    
            Nodo_des_iz.derecho = Nodo_des_iz_der_iz;
            Nodo_des.izquierdo = Nodo_des_iz_der_der;
            Nodo_des_iz_der.izquierdo = Nodo_des_iz;
            Nodo_des_iz_der.derecho = Nodo_des;

            Nodo_des_iz_der.padre = Padre;
            Nodo_des.padre = Nodo_des_iz.padre = Nodo_des_iz_der;
            if (Nodo_des_iz_der_iz != null)
                Nodo_des_iz_der_iz.padre = Nodo_des_iz;
            if (Nodo_des_iz_der_der != null)
                Nodo_des_iz_der_der.padre = Nodo_des;


          
            switch (Nodo_des_iz_der.f_e)
            {
                case -1:
                    {
                        Nodo_des_iz.f_e = 0;
                        Nodo_des.f_e = 1;
                    }
                    break;

                case 0:
                    {
                        Nodo_des_iz.f_e = 0;
                        Nodo_des.f_e = 0;
                    }
                    break;

                case 1:
                    {
                        Nodo_des_iz.f_e = -1;
                        Nodo_des.f_e = 0;
                    }
                    break;
            }
            Nodo_des_iz_der.f_e = 0;
            Nuevolog.ROTDOBDER(w, nodo.llave);
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

                Nuevolog.incercion(w,_nuevo.llave);
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
                    actualizar_factores(_actual, "derecho", true);
                    Nuevolog.incercion(w,_nuevo.llave);

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
                    actualizar_factores(_actual, "izquierdo", true);
                    Nuevolog.incercion(w, _nuevo.llave);
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
    

