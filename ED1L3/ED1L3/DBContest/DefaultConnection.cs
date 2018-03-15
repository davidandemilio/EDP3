using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDA;
namespace ED1L3.DBContest
{
    public class DefaultConnection<T,K>
    {
        private static volatile DefaultConnection<T,K> Instance;
        private static object syncRoot = new Object();

        public List<T> datos = new List<T>();
        public List<string> Ids = new List<string>();
        public int IDActual { get; set; }

        public ArbolBinarioBusqueda<T,K> AB = new ArbolBinarioBusqueda<T,K>();


        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection<T,K> getInstance
        {

            get
            {

                if (Instance == null)
                {
                    lock (syncRoot)
                    {

                        if (Instance == null)
                        {
                            Instance = new DefaultConnection<T,K>();
                        }
                    }
                }
                return Instance;
            }
        }


    }
}