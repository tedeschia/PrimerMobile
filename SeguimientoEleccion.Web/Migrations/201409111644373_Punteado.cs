namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Punteado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Electors", "Punteado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Electors", "Punteado");
        }
    }
}
