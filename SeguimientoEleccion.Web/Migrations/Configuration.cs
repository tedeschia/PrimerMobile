using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            context.Colegios.AddOrUpdate(c => c.Nombre,
                new[]
                {
                    new Colegio {Nombre = "ALTO VALLE OESTE (RIO NEGRO)", Usuario = "Alto85554"},
                    new Colegio {Nombre = "AVELLANEDA-LANUS", Usuario = "Avel77948"},
                    new Colegio {Nombre = "AZUL", Usuario = "Azul17904"},
                    new Colegio {Nombre = "BAHIA BLANCA", Usuario = "Bahi19136"},
                    new Colegio {Nombre = "BARILOCHE", Usuario = "Bari79168"},
                    new Colegio {Nombre = "BELL VILLE", Usuario = "Bell71538"},
                    new Colegio {Nombre = "CATAMARCA", Usuario = "Cata51641"},
                    new Colegio {Nombre = "CHARATA", Usuario = "Char56534"},
                    new Colegio {Nombre = "CHOS MALAL", Usuario = "Chos89274"},
                    new Colegio {Nombre = "COMODORO RIVADAVIA", Usuario = "Como82059"},
                    new Colegio {Nombre = "CONC.DEL URUGUAY-ENTRE RIOS", Usuario = "Conc23779"},
                    new Colegio {Nombre = "CONCARAN", Usuario = "Conc63690"},
                    new Colegio {Nombre = "CORDOBA", Usuario = "Cord37077"},
                    new Colegio {Nombre = "CORRIENTES", Usuario = "Corr70733"},
                    new Colegio {Nombre = "CRUZ DEL EJE", Usuario = "Cruz66514"},
                    new Colegio {Nombre = "CURUZU CUATIA", Usuario = "Curu34244"},
                    new Colegio {Nombre = "CUTRAL-CO", Usuario = "Cutr42854"},
                    new Colegio {Nombre = "DOLORES", Usuario = "Dolo40317"},
                    new Colegio {Nombre = "EL DORADO", Usuario = "Eldo50520"},
                    new Colegio {Nombre = "ESQUEL", Usuario = "Esqu30863"},
                    new Colegio {Nombre = "FORMOSA", Usuario = "Form10249"},
                    new Colegio {Nombre = "GENERAL ROCA ( RIO NEGRO)", Usuario = "Gene61214"},
                    new Colegio {Nombre = "GOYA- CORRIENTES", Usuario = "Goya85729"},
                    new Colegio {Nombre = "Gral. San Martín (MZA)", Usuario = "Gral52618"},
                    new Colegio {Nombre = "Gral. Sarmiento (Chubut)", Usuario = "Gral64595"},
                    new Colegio {Nombre = "JUJUY", Usuario = "Juju41400"},
                    new Colegio {Nombre = "JUNIN", Usuario = "Juni49476"},
                    new Colegio {Nombre = "JUNIN DE LOS ANDES", Usuario = "Juni20455"},
                    new Colegio {Nombre = "LA MATANZA", Usuario = "Lama21346"},
                    new Colegio {Nombre = "LA PAMPA", Usuario = "Lapa65771"},
                    new Colegio {Nombre = "LA PLATA", Usuario = "Lapl22869"},
                    new Colegio {Nombre = "LA RIOJA", Usuario = "Lari47776"},
                    new Colegio {Nombre = "LABOULAGE", Usuario = "Labo14441"},
                    new Colegio {Nombre = "LOMAS DE ZAMORA", Usuario = "Loma16441"},
                    new Colegio {Nombre = "MAR DEL PLATA", Usuario = "Mard32510"},
                    new Colegio {Nombre = "MARCOS JUAREZ", Usuario = "Marc35254"},
                    new Colegio {Nombre = "MENDOZA", Usuario = "Mend64537"},
                    new Colegio {Nombre = "MERCEDES", Usuario = "Merc61136"},
                    new Colegio {Nombre = "MORENO-RODRIGUEZ", Usuario = "More62639"},
                    new Colegio {Nombre = "MORON", Usuario = "Moro73519"},
                    new Colegio {Nombre = "NECOCHEA", Usuario = "Neco26224"},
                    new Colegio {Nombre = "NEUQUEN", Usuario = "Neuq62359"},
                    new Colegio {Nombre = "OBERA (MISIONES)", Usuario = "Ober67627"},
                    new Colegio {Nombre = "PARANA- ENTRE RIOS", Usuario = "Para66800"},
                    new Colegio {Nombre = "PASO DE LOS LIBRES", Usuario = "Paso73030"},
                    new Colegio {Nombre = "PERGAMINO", Usuario = "Perg36166"},
                    new Colegio {Nombre = "POSADAS ( MISIONES)", Usuario = "Posa63597"},
                    new Colegio {Nombre = "PTE. SAENZ PEÑA (CHACO)", Usuario = "Pte.58970"},
                    new Colegio {Nombre = "PUERTO MADRYN", Usuario = "Puer11913"},
                    new Colegio {Nombre = "Quilmes", Usuario = "Quil83048"},
                    new Colegio {Nombre = "Rafaela", Usuario = "Rafa17042"},
                    new Colegio {Nombre = "Reconquista", Usuario = "Reco28203"},
                    new Colegio {Nombre = "RESISTENCIA", Usuario = "Resi27728"},
                    new Colegio {Nombre = "Río Cuarto (Córdoba)", Usuario = "Ríoc67483"},
                    new Colegio {Nombre = "Río Gallegos", Usuario = "Ríog34819"},
                    new Colegio {Nombre = "Río Grande", Usuario = "Ríog65677"},
                    new Colegio {Nombre = "Río Tercero", Usuario = "Ríot32500"},
                    new Colegio {Nombre = "Rosario", Usuario = "Rosa60053"},
                    new Colegio {Nombre = "Salta", Usuario = "Salt32708"},
                    new Colegio {Nombre = "San Francisco (CORDOBA)", Usuario = "Sanf26216"},
                    new Colegio {Nombre = "San Isidro", Usuario = "Sani71775"},
                    new Colegio {Nombre = "SAN JUAN", Usuario = "Sanj58411"},
                    new Colegio {Nombre = "San Luis", Usuario = "Sanl80156"},
                    new Colegio {Nombre = "San Martín", Usuario = "Sanm46338"},
                    new Colegio {Nombre = "San Nicolás", Usuario = "Sann83707"},
                    new Colegio {Nombre = "San Rafael", Usuario = "Sanr76013"},
                    new Colegio {Nombre = "Santa Fé", Usuario = "Sant50857"},
                    new Colegio {Nombre = "Santo Tomé (Corrientes)", Usuario = "Sant35397"},
                    new Colegio {Nombre = "Stgo. Del Estero", Usuario = "Stgo12095"},
                    new Colegio {Nombre = "Trelew (Chubut)", Usuario = "Trel59130"},
                    new Colegio {Nombre = "Trenque Lauquen", Usuario = "Tren16428"},
                    new Colegio {Nombre = "Tucumán", Usuario = "Tucu59671"},
                    new Colegio {Nombre = "TUCUMAN DEL SUR", Usuario = "Tucu23221"},
                    new Colegio {Nombre = "Ushuaia", Usuario = "Ushu80721"},
                    new Colegio {Nombre = "VALLE DE UCO", Usuario = "Vall29742"},
                    new Colegio {Nombre = "VENADO TUERTO", Usuario = "Vena21306"},
                    new Colegio {Nombre = "VIEDMA", Usuario = "Vied46506"},
                    new Colegio {Nombre = "VILLA DOLORES", Usuario = "Vill12721"},
                    new Colegio {Nombre = "VILLA MARIA", Usuario = "Vill50779"},
                    new Colegio {Nombre = "VILLA MERCEDES", Usuario = "Vill21947"},
                    new Colegio {Nombre = "ZAPALA", Usuario = "Zapa15402"},
                    new Colegio {Nombre = "ZARATE- CAMPANA", Usuario = "Zara44068"},
                });
        }
        bool AddUserAndRole(SeguimientoEleccion.Web.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("fiscal"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            var usuarios = new string[]
                           {
                               ""
                           };

            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };
            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }
    }
}
