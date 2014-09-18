namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSync : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Electors", "InSync", c => c.Boolean(nullable: false));
            Sql(@"update Electors set InSync = 1");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Electors", "InSync");
        }
    }
}
