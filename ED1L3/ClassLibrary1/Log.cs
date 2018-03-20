using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDA
{
    class Log
    {
        public void Logg(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry: ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine(" :");
            w.WriteLine(" :{0}", logMessage);
            w.WriteLine("---------------------");
        }
        public static void Dumplog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        public void incercion(StreamWriter W, StreamReader R)
        {
            Logg("Incercion", W);
            Dumplog(R);
        }
        public void incercionINT(StreamWriter W, StreamReader R)
        {
            Logg("Incercion Interna", W);
            Dumplog(R);
        }
        public void Eliminacion(StreamWriter W, StreamReader R)
        {
            Logg("Eliminacion", W);
            Dumplog(R);
        }
        public void EliminacionINT(StreamWriter W, StreamReader R)
        {
            Logg("Eliminacion interna", W);
            Dumplog(R);
        }
        public void ActualizarVal(StreamWriter W, StreamReader R)
        {
            Logg("Actualizar factores de Balaceo", W);
            Dumplog(R);
        }
        public void Balanceo(StreamWriter W, StreamReader R)
        {
            Logg("Balanceo", W);
            Dumplog(R);
        }
        public void ROTSIMPDER(StreamWriter W, StreamReader R)
        {
            Logg("Rotacion simpe a la derecha", W);
            Dumplog(R);
        }
        public void ROTSIMPIZQ(StreamWriter W, StreamReader R)
        {
            Logg("Rotacion simpe a la izquierda", W);
            Dumplog(R);
        }
        public void ROTDOBDER(StreamWriter W, StreamReader R)
        {
            Logg("Rotacion doble a la derecha", W);
            Dumplog(R);
        }
        public void ROTDOBIZQ(StreamWriter W, StreamReader R)
        {
            Logg("Rotacion doble a la izquierda", W);
            Dumplog(R);
        }
    }
}
