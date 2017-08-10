using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace GdNet.IdentitySrv.Configuration
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Enabled = true,
                    Username = "huanhvtest1@gmail.com",
                    Password = "Abc123456",
                    Subject = "huanhvtest1@gmail.com",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Email, "huanhvtest1@gmail.com"),
                        new Claim(Constants.ClaimTypes.Role, "sysadmin"),
                    },
                }
            };
        }
    }
}