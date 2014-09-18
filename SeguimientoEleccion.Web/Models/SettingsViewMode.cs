using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeguimientoEleccion.Web.Models
{
    public class SettingsViewMode
    {
        public ServicioEstadoEnum ServicioEstado { get; set; }
    }

    public enum ServicioEstadoEnum
    {
        Habilitado,
        Deshabilitado,
        Timeout
    }
}