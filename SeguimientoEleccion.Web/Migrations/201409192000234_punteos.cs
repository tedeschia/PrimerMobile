namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class punteos : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Electors", "Punteado", "Voto");

        }
        
        public override void Down()
        {
            RenameColumn("dbo.Electors", "Voto", "Punteado");
            AddColumn("dbo.Electors", "Punteado", c => c.Boolean(nullable: false));
            DropColumn("dbo.Electors", "ColegioId");
        }
    }
}
