namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Colegios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colegios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        UltimaActualizacionPadron = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            Sql(@"insert into Colegios (Nombre, UltimaActualizacionPadron) VALUES ('Lomas de Zamora','2014-09-01')");
        }
        
        public override void Down()
        {
            DropTable("dbo.Colegios");
        }
    }
}
