using Business.Abstract;
using Entity;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Identity
{
    public static class IdentitySeed
    {
        public async static void Seed(IApplicationBuilder app,IConfiguration configuration)
        {
            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var cartService = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ICartService>();

            var adminUserName = configuration["IdentitySetting:Admin:UserName"];           
            var adminUser = await userManager.FindByNameAsync(adminUserName!);

            if(adminUser == null)
            {
                var adminEmail = configuration["IdentitySetting:Admin:Email"];
                var adminPassword = configuration["IdentitySetting:Admin:Password"];

                adminUser = new ApplicationUser()
                {
                    UserName = adminUserName,
                    FirstName = adminUserName,
                    LastName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser,adminPassword!);
                await roleManager.CreateAsync(new IdentityRole{Name=adminUserName});
                await userManager.AddToRoleAsync(adminUser,adminUserName!);
                await cartService.CreateAsync(new Cart{UserId=adminUser.Id});

                var customerUserName = configuration["IdentitySetting:Customer:UserName"];
                var customerEmail = configuration["IdentitySetting:Customer:Email"];
                var customerPassword = configuration["IdentitySetting:Customer:Password"];

                var customerUser = new ApplicationUser()
                {
                    UserName = customerUserName,
                    FirstName = customerUserName,
                    LastName = customerUserName,
                    Email = customerEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(customerUser,customerPassword!);
                await roleManager.CreateAsync(new IdentityRole{Name=customerUserName});
                await userManager.AddToRoleAsync(customerUser,customerUserName!);
                await cartService.CreateAsync(new Cart{UserId=customerUser.Id});
            }
        }
    }
}