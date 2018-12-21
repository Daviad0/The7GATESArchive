namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Keys : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserGate");
            AddPrimaryKey("dbo.UserGate", new[] { "GateID", "UserID" });
            DropColumn("dbo.UserGate", "UserGateID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserGate", "UserGateID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.UserGate");
            AddPrimaryKey("dbo.UserGate", "UserGateID");
        }
    }
}
