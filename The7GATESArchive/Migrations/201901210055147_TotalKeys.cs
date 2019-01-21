namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TotalKeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gate", "Keys", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gate", "Keys");
        }
    }
}
