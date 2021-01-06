using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentOrganization.Models
{
    //IdentityDbContext<ApplicationUser>
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Sport_Club> Clubs { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<RequestOrganization> RequestOrganizations { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Coach>()
                .HasOptional(c => c.Sport_Club)
                .WithOptionalDependent(s => s.Coach);

            modelBuilder.Entity<ApplicationUser>().HasMany(s => s.organizations).WithMany(o => o.students)
                .Map(t => t.MapLeftKey("StudentID").MapRightKey("OrganizationID").ToTable("StudentOrganization"));
        }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<StudentOrganization.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}