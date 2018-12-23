namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecificGate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGate", "Keys", c => c.Int(nullable: false));
            DropColumn("dbo.User", "Keys");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Keys", c => c.Int(nullable: false));
            DropColumn("dbo.UserGate", "Keys");
        }
    }
}
