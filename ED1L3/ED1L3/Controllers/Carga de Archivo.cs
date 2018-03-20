using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using directorios = System.IO;
using TDA;
using ED1L3.Models;

namespace ED1L3.Controllers
{
    public class Carga_de_Archivo<T, K>
    {
        /// <summary>
        /// Metodo que descompone un archivo Json y lo almacena en un arbol binario generico.
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="SERVIDOR"></param>
        /// <returns></returns>
        /// 
        public List<Nodo<T, K>> Cargajsoninterna(HttpPostedFileBase archivo, HttpServerUtilityBase SERVIDOR)
        {
            //ArbolBinarioBusqueda<T, K> arbol_a_insertar = new ArbolBinarioBusqueda<T, K>();
            List<Nodo<T,K>> nodos_a_insertar = new List<Nodo<T,K>>();
            Partido _partido = new Partido();
            Nodo<T, K> nuevo;

            string pathArchivo = string.Empty;
            if (archivo != null)
            {
                string path = SERVIDOR.MapPath("~/Cargas/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pathArchivo = path + Path.GetFileName(archivo.FileName);
                string extension = Path.GetExtension(archivo.FileName);
                archivo.SaveAs(pathArchivo);
                string archivoJSON = directorios.File.ReadAllText(pathArchivo);
                string[] nodos = archivoJSON.Split('{', '}');
                for (int i = 2; i < nodos.Length - 1; i += 2)
                {
                    nuevo = JsonConvert.DeserializeObject<Nodo<T, K>>("{\r\n " + "\"valor\":{" + nodos[i] + "}");
                    nodos_a_insertar.Add(nuevo);
                    //arbol_a_insertar.Insertar(nuevo);
                }
                return nodos_a_insertar;
            }
            return null;
        }
    }
}