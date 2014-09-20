namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarPunteo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Electors", "Punteado", c => c.Boolean(nullable: false));
            AddColumn("dbo.Electors", "Referente", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Electors", "Referente");
            DropColumn("dbo.Electors", "Punteado");
        }
    }
}
