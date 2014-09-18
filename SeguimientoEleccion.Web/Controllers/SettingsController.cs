using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using SeguimientoEleccion.Web.Models;

namespace SeguimientoEleccion.Web.Controllers
{
    [RoutePrefix("api/settings")]
    public class SettingsController : ApiController
    {
        [Route("servicio")]
        public IHttpActionResult GetEstadoServicio()
        {
            var estadoServicio = GetEstadoServicioSetting();
            return Ok(estadoServicio);
        }
        [Route("servicio")]
        public IHttpActionResult PutEstadoServicio(ServicioEstadoEnum estadoServicio)
        {
            SetEstadoServicioSetting(estadoServicio);
            return Ok();
        }

        private ServicioEstadoEnum GetEstadoServicioSetting()
        {
            ServicioEstadoEnum setting;
            if (ServicioEstadoEnum.TryParse(System.Configuration.ConfigurationSettings.AppSettings["EstadoServicio"],
                out setting))
            {
                return setting;
            }
            else
            {
                return ServicioEstadoEnum.Habilitado;
            };
        }
        private void SetEstadoServicioSetting(ServicioEstadoEnum setting)
        {
            System.Configuration.ConfigurationSettings.AppSettings["EstadoServicio"] = setting.ToString();
        }
    }
}