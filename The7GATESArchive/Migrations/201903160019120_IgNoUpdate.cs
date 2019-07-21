namespace The7GATESArchive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IgNoUpdate : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Update");
        }
        
        public override void Down()
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
    }
}
