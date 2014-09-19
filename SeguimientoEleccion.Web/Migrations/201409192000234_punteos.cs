namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class punteos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Punteos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DNI = c.Int(nullable: false),
                        Referente = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DNI);
            
            AddColumn("dbo.Electors", "ColegioId", c => c.Int(nullable: false));
            RenameColumn("dbo.Electors", "Punteado", "Voto");

        }
        
        public override void Down()
        {
            RenameColumn("dbo.Electors", "Voto", "Punteado");
            AddColumn("dbo.Electors", "Punteado", c => c.Boolean(nullable: false));
            DropIndex("dbo.Punteos", new[] { "DNI" });
            DropColumn("dbo.Electors", "ColegioId");
            DropTable("dbo.Punteos");
        }
    }
}
