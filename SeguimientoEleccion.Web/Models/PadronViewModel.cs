using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguimientoEleccion.Web.Models
{
    public class PadronViewModel
    {
        public DateTime FechaUltimaActualizacion { get; set; }
        public IEnumerable<Elector> Padron { get; set; }
    }
}