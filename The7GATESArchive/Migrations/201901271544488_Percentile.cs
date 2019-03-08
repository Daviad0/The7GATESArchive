namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Percentile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGate", "Percentile", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserGate", "Percentile");
        }
    }
}
