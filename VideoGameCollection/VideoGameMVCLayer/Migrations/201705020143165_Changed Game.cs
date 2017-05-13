namespace VideoGameMVCLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedGame : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Games");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Console = c.String(),
                        Developer = c.String(),
                        Publisher = c.String(),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
