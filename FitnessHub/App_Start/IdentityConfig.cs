using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using FitnessHub.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        // Configure the db context, user manager, and role manager to use a single instance per request
        app.CreatePerOwinContext(ApplicationDbContext.Create);
        app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateUserManager);
        app.CreatePerOwinContext<RoleManager<IdentityRole>>(CreateRoleManager);

        // Configure the application for OAuth-based flow
        app.UseCookieAuthentication(new CookieAuthenticationOptions
        {
            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            LoginPath = new PathString("/Account/Login"),
        });

        app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

        // Additional configurations if needed
    }

    private UserManager<ApplicationUser> CreateUserManager(IdentityFactoryOptions<UserManager<ApplicationUser>> options, IOwinContext context)
    {
        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
        // Configure user manager settings if needed
        return userManager;
    }

    private RoleManager<IdentityRole> CreateRoleManager(IdentityFactoryOptions<RoleManager<IdentityRole>> options, IOwinContext context)
    {
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        // Configure role manager settings if needed
        return roleManager;
    }
}
