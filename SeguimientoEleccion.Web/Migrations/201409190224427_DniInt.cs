namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DniInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Electors", "DNI", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Electors", "DNI", c => c.Double(nullable: false));
        }
    }
}
