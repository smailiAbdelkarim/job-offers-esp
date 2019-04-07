using esp_test.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(esp_test.Startup))]
namespace esp_test
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers() {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Admins")) {
                role.Name = "Admins";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "karim";
                user.Email = "abdelkarimsmaili709@gmail.com";
                var check = userManager.Create(user, "Karim@2019");
                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admins");

                }



            }
        }
    }
}
