namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarPunteo : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Punteos", new[] { "DNI" });
            AddColumn("dbo.Electors", "Punteado", c => c.Boolean(nullable: false));
            AddColumn("dbo.Electors", "Referente", c => c.String());
            DropTable("dbo.Punteos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Punteos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DNI = c.Int(nullable: false),
                        Referente = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Electors", "Referente");
            DropColumn("dbo.Electors", "Punteado");
            CreateIndex("dbo.Punteos", "DNI");
        }
    }
}
