namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gate",
                c => new
                    {
                        GateID = c.Int(nullable: false, identity: true),
                        Theme = c.String(),
                    })
                .PrimaryKey(t => t.GateID);
            
            CreateTable(
                "dbo.UserGate",
                c => new
                    {
                        UserGateID = c.Int(nullable: false, identity: true),
                        GateID = c.Int(nullable: false),
                        UserID = c.Guid(nullable: false),
                        Time = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.UserGateID)
                .ForeignKey("dbo.Gate", t => t.GateID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.GateID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Username = c.String(),
                        Keys = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGate", "UserID", "dbo.User");
            DropForeignKey("dbo.UserGate", "GateID", "dbo.Gate");
            DropIndex("dbo.UserGate", new[] { "UserID" });
            DropIndex("dbo.UserGate", new[] { "GateID" });
            DropTable("dbo.User");
            DropTable("dbo.UserGate");
            DropTable("dbo.Gate");
        }
    }
}
