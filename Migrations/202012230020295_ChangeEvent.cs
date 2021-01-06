namespace StudentOrganization.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "picture_url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "picture_url");
        }
    }
}
