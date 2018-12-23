namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTotalTimeAndKeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Keys", c => c.Int(nullable: false));
            AddColumn("dbo.User", "TimeForAllGates", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.User", "Rank", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Rank");
            DropColumn("dbo.User", "TimeForAllGates");
            DropColumn("dbo.User", "Keys");
        }
    }
}
