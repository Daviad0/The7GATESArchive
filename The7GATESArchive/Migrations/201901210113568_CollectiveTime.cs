namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CollectiveTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGate", "CollectiveTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserGate", "CollectiveTime");
        }
    }
}
