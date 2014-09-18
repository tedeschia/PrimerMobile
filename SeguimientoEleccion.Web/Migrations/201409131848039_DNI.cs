namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DNI : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Electors", "DNI", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Electors", "DNI");
        }
    }
}
