namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPercentile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Percentile", c => c.Single());
            AddColumn("dbo.User", "PrizeStatus", c => c.String());
            AlterColumn("dbo.UserGate", "Percentile", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserGate", "Percentile", c => c.Single(nullable: false));
            DropColumn("dbo.User", "PrizeStatus");
            DropColumn("dbo.User", "Percentile");
        }
    }
}
