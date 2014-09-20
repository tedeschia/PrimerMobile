namespace SeguimientoEleccion.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColegioId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Electors", "ColegioId", c => c.Int(nullable: false));
            Sql(@"
update electors set colegioid = (select id from colegios where nombre = electors.colegio)
");
            CreateIndex("dbo.Electors", "ColegioId");
            AddForeignKey("dbo.Electors", "ColegioId", "dbo.Colegios", "Id", cascadeDelete: true);
            DropColumn("dbo.Electors", "Colegio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Electors", "Colegio", c => c.String());
            DropForeignKey("dbo.Electors", "ColegioId", "dbo.Colegios");
            DropIndex("dbo.Electors", new[] { "ColegioId" });
        }
    }
}
