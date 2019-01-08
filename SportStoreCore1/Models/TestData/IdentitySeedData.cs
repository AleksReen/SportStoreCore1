using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportStoreCore1.Models.DbContexts;

namespace SportStoreCore1.Models.TestData
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Aleks";
        private const string adminPassword = "Secret123$";

        public static async void EnsurePopulated(IApplicationBuilder app) {


            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
             
                var user = await userManager.FindByIdAsync(adminUser);

                if (user == null)
                {
                    user = new IdentityUser("Aleks");
                    await userManager.CreateAsync(user, adminPassword);
                }
            }
        }
    }
}
