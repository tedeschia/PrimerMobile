using SeguimientoEleccion.Web.Models;

namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SeguimientoEleccion.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SeguimientoEleccion.Web.Models.ApplicationDbContext context)
        {
        }
    }
}
