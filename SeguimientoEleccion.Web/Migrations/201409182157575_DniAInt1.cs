namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DniAInt1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Electors", "DNI", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Electors", "DNI", c => c.Int(nullable: false));
        }
    }
}
