using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SeguimientoEleccion.Web.Migrations;

namespace SeguimientoEleccion.Web.Models
{
    public class Elector
    {
        public Elector()
        {
        }
        public int Id { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public int ColegioId { get; set; }
        public Colegio Colegio { get; set; }
        public bool Voto { get; set; }
        public int Mesa { get; set; }
        public bool Punteado { get; set; }
        public string Referente { get; set; }
    }
}