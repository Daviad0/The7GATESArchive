namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Realtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Realtime", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Realtime");
        }
    }
}
