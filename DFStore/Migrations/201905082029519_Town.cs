namespace DFStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Town : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ContactNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ContactNumber");
        }
    }
}
