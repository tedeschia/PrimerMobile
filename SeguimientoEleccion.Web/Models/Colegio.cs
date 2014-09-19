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
        public string Usuario { get; set; }
        public ICollection<Elector> Electores { get; set; }
    }
}