namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Unsupported : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Unsupported", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Unsupported");
        }
    }
}
