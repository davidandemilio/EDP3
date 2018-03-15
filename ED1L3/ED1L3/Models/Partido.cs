using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ED1L3.Models
{
    public class Partido
    {   public int Nopartido { get; set; }

       public DateTime FechaPartido { get; set; }
        public string Grupo { get; set; }
        public string Pais_1 { get; set; }
        public string Pais_2 { get; set; }
        public string Estadio { get; set; }

    }
}