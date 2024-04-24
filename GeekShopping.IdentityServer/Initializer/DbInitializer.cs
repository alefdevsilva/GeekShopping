using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly SQLServerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            SQLServerContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initializer()
        {
            if (_roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;

            _roleManager.CreateAsync(new IdentityRole(
                IdentityConfiguration.Admin)).GetAwaiter().GetResult();

            _roleManager.CreateAsync(new IdentityRole(
                IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "alef-admin",
                Email = "alef-admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11)94242-9292",
                FirstName = "Alef",
                LastName = "Admin"
            };

            _userManager.CreateAsync(admin, "Teste123$").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName}{admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;
            
            
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "alef-client",
                Email = "alef-client@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (11)94242-9292",
                FirstName = "Alef",
                LastName = "Client"
            };

            _userManager.CreateAsync(client, "Teste123$").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

            var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName}{client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;
        }
    }
}
