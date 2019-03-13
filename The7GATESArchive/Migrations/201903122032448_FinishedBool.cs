namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinishedBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGate", "Finished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserGate", "Finished");
        }
    }
}
