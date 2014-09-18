using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguimientoEleccion.Web.Models
{
    public class Colegio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime UltimaActualizacionPadron { get; set; }

    }
}