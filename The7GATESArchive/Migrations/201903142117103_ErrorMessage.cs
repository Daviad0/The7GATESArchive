namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ErrorMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "GateError", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "GateError");
        }
    }
}
