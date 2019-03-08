namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Insights2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Insight1", c => c.Int(nullable: false));
            AddColumn("dbo.User", "Insight2", c => c.Int(nullable: false));
            AddColumn("dbo.User", "Insight3", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Insight3");
            DropColumn("dbo.User", "Insight2");
            DropColumn("dbo.User", "Insight1");
        }
    }
}
