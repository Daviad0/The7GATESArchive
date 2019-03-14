namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Update",
                c => new
                    {
                        TimeStamp = c.DateTime(nullable: false),
                        Gate = c.Int(nullable: false),
                        AdditionalInfo = c.String(),
                    })
                .PrimaryKey(t => t.TimeStamp);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Update");
        }
    }
}
