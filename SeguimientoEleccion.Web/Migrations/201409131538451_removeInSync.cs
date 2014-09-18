namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeInSync : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Electors", "InSync");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Electors", "InSync", c => c.Boolean(nullable: false));
        }
    }
}
