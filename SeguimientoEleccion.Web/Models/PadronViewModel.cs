using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguimientoEleccion.Web.Models
{
    public class PadronViewModel
    {
        public DateTime FechaUltimaActualizacion { get; set; }
        public IEnumerable<ElectorViewModel> Padron { get; set; }

        public string Colegio { get; set; }
        public string Usuario { get; set; }

        public class ElectorViewModel
        {
            public int Id { get; set; }
            public int DNI { get; set; }
            public string Nombre { get; set; }
            public bool Voto { get; set; }
        }
    }
}