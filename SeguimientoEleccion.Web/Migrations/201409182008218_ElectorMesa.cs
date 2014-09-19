namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ElectorMesa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Electors", "Mesa", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Electors", "Mesa");
        }
    }
}
