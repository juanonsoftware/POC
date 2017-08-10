using IdentityServer3.AspNetIdentity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GdNet.IdentitySrv
{
    public static class UserServiceFactory
    {
        public static AspNetIdentityUserService<IdentityUser, string> Create()
        {
            var context = new IdentityDbContext("AspNetIdentity");
            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);

            return new AspNetIdentityUserService<IdentityUser, string>(userManager);
        }
    }
}