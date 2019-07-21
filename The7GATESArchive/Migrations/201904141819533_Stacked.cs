namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stacked : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGate", "Stacked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserGate", "Stacked");
        }
    }
}
