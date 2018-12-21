namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGate", "Rank", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserGate", "Rank");
        }
    }
}
