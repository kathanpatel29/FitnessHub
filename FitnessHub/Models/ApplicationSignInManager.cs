using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using FitnessHub.Models;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System;

public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
{
    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
    {
    }

    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    {
        return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    }

    internal async Task<SignInStatus> ExternalSignInAsync(UserLoginInfo login, bool isPersistent)
    {
        throw new NotImplementedException();
    }
}
