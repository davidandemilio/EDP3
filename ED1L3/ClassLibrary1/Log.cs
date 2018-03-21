using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA.Interfaces;

namespace TDA
{
    public class Log<K>
    {

        public Convertirastring<K> convsersor;
        StreamWriter f;
        public void Logg(string logMessage, StreamWriter w)
        {

            f = w;
           
            w.WriteLine(logMessage);
           
        }


        public void close( )
        {


            f.Close();


        }

        public void incercion(StreamWriter W,K llave)
        {
            Logg("Incercion: "+ convsersor(llave), W);
         
        }
   
        public void Eliminacion(StreamWriter W, K llave)
        {
            Logg("Eliminacion: " + convsersor(llave), W);

        }
    
        public void ActualizarVal(StreamWriter W, K llave)
        {
            Logg("Actualizar factores de Balaceo: " + convsersor(llave), W);
 
        }
        public void Balanceo(StreamWriter W, K llave)
        {
            Logg("Balanceo: " + convsersor(llave), W);
      
        }
        public void ROTSIMPDER(StreamWriter W, K llave)
        {
            Logg("Rotacion simpe a la derecha: " + convsersor(llave), W);
     
        }
        public void ROTSIMPIZQ(StreamWriter W, K llave)
        {
            Logg("Rotacion simpe a la izquierda: " + convsersor(llave), W);
     
        }
        public void ROTDOBDER(StreamWriter W, K llave)
        {
            Logg("Rotacion doble a la derecha: " + convsersor(llave), W);
       
        }
        public void ROTDOBIZQ(StreamWriter W, K llave)
        {
            Logg("Rotacion doble a la izquierda: " + convsersor(llave), W);
    
        }
    }
}
