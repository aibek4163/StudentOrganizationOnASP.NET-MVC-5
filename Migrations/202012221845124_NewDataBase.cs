namespace StudentOrganization.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sport_Club",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        coach_id = c.Long(nullable: false),
                        student_id = c.Long(nullable: false),
                        type_sport = c.String(nullable: false),
                        club_name = c.String(nullable: false),
                        players_amount = c.Int(nullable: false),
                        founded_year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        surname = c.String(nullable: false),
                        login = c.String(nullable: false),
                        password = c.String(nullable: false),
                        name_of_sport = c.String(nullable: false),
                        Sport_Club_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Sport_Club", t => t.Sport_Club_id)
                .Index(t => t.Sport_Club_id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        surname = c.String(),
                        login = c.String(),
                        password = c.String(),
                        group_id = c.Long(nullable: false),
                        club_id = c.Long(nullable: false),
                        club_id1 = c.Long(),
                        group_id1 = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Sport_Club", t => t.club_id1)
                .ForeignKey("dbo.Groups", t => t.group_id1)
                .Index(t => t.club_id1)
                .Index(t => t.group_id1);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        group_name = c.String(nullable: false),
                        courseId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Courses", t => t.courseId, cascadeDelete: true)
                .Index(t => t.courseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        number_course = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        leader_id = c.String(),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        picture_url = c.String(nullable: false),
                        student_lead_Id = c.String(maxLength: 128),
                        Student_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.student_lead_Id)
                .ForeignKey("dbo.Students", t => t.Student_id)
                .Index(t => t.student_lead_Id)
                .Index(t => t.Student_id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        picture_url = c.String(),
                        group_id = c.Long(nullable: false),
                        club_id = c.Long(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        club_id1 = c.Long(),
                        group_id1 = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sport_Club", t => t.club_id1)
                .ForeignKey("dbo.Groups", t => t.group_id1)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.club_id1)
                .Index(t => t.group_id1);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        date = c.DateTime(nullable: false),
                        description = c.String(nullable: false),
                        org_id_id = c.Long(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Organizations", t => t.org_id_id)
                .Index(t => t.org_id_id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        short_description = c.String(nullable: false),
                        description = c.String(nullable: false),
                        picture_url = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.RequestOrganizations",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        orgID = c.Long(nullable: false),
                        studentID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Organizations", t => t.orgID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.studentID)
                .Index(t => t.orgID)
                .Index(t => t.studentID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.StudentOrganization",
                c => new
                    {
                        StudentID = c.String(nullable: false, maxLength: 128),
                        OrganizationID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentID, t.OrganizationID })
                .ForeignKey("dbo.AspNetUsers", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.OrganizationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RequestOrganizations", "studentID", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequestOrganizations", "orgID", "dbo.Organizations");
            DropForeignKey("dbo.Events", "org_id_id", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "Student_id", "dbo.Students");
            DropForeignKey("dbo.Organizations", "student_lead_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentOrganization", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.StudentOrganization", "StudentID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "group_id1", "dbo.Groups");
            DropForeignKey("dbo.AspNetUsers", "club_id1", "dbo.Sport_Club");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Students", "group_id1", "dbo.Groups");
            DropForeignKey("dbo.Groups", "courseId", "dbo.Courses");
            DropForeignKey("dbo.Students", "club_id1", "dbo.Sport_Club");
            DropForeignKey("dbo.Coaches", "Sport_Club_id", "dbo.Sport_Club");
            DropIndex("dbo.StudentOrganization", new[] { "OrganizationID" });
            DropIndex("dbo.StudentOrganization", new[] { "StudentID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RequestOrganizations", new[] { "studentID" });
            DropIndex("dbo.RequestOrganizations", new[] { "orgID" });
            DropIndex("dbo.Events", new[] { "org_id_id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "group_id1" });
            DropIndex("dbo.AspNetUsers", new[] { "club_id1" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Organizations", new[] { "Student_id" });
            DropIndex("dbo.Organizations", new[] { "student_lead_Id" });
            DropIndex("dbo.Groups", new[] { "courseId" });
            DropIndex("dbo.Students", new[] { "group_id1" });
            DropIndex("dbo.Students", new[] { "club_id1" });
            DropIndex("dbo.Coaches", new[] { "Sport_Club_id" });
            DropTable("dbo.StudentOrganization");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RequestOrganizations");
            DropTable("dbo.News");
            DropTable("dbo.Events");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Organizations");
            DropTable("dbo.Courses");
            DropTable("dbo.Groups");
            DropTable("dbo.Students");
            DropTable("dbo.Coaches");
            DropTable("dbo.Sport_Club");
        }
    }
}
