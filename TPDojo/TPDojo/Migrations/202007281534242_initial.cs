namespace TPDojo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arme",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nom = c.String(),
                        Degats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Samourai",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Force = c.Int(nullable: false),
                        Nom = c.String(),
                        Arme_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Arme", t => t.Arme_Id)
                .Index(t => t.Arme_Id);
            
            CreateTable(
                "dbo.ArtMartial",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SamouraiArtMartial",
                c => new
                    {
                        Samourai_Id = c.Long(nullable: false),
                        ArtMartial_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Samourai_Id, t.ArtMartial_Id })
                .ForeignKey("dbo.Samourai", t => t.Samourai_Id, cascadeDelete: true)
                .ForeignKey("dbo.ArtMartial", t => t.ArtMartial_Id, cascadeDelete: true)
                .Index(t => t.Samourai_Id)
                .Index(t => t.ArtMartial_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SamouraiArtMartial", "ArtMartial_Id", "dbo.ArtMartial");
            DropForeignKey("dbo.SamouraiArtMartial", "Samourai_Id", "dbo.Samourai");
            DropForeignKey("dbo.Samourai", "Arme_Id", "dbo.Arme");
            DropIndex("dbo.SamouraiArtMartial", new[] { "ArtMartial_Id" });
            DropIndex("dbo.SamouraiArtMartial", new[] { "Samourai_Id" });
            DropIndex("dbo.Samourai", new[] { "Arme_Id" });
            DropTable("dbo.SamouraiArtMartial");
            DropTable("dbo.ArtMartial");
            DropTable("dbo.Samourai");
            DropTable("dbo.Arme");
        }
    }
}
