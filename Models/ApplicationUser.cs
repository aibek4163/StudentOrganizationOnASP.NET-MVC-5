using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudentOrganization.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Name")]
        //[Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        //[Required(ErrorMessage = "Surname Required")]
        public string Surname { get; set; }

        [Display(Name = "Picture URL")]
        public string picture_url { get; set; }

        [Display(Name = "Group")]
        [Required(ErrorMessage = "Group Required")]
        public long group_id { get; set; }
        public Group group { get; set; }        
        public long organization_id { get; set; }
        public List<Organization> organizations { get; set; }
        public long club_id { get; set; }
        public Sport_Club club { get; set; }


        public ApplicationUser()
        {
            organizations = new List<Organization>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }

        
    }
}