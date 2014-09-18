using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SeguimientoEleccion.Web.Models
{
    public class Elector
    {
        public Elector()
        {
        }
        public int Id { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Colegio { get; set; }
        public bool Punteado { get; set; }
    }
}