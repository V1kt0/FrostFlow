using Microsoft.AspNetCore.Identity;

namespace FrostFlow.Data.Seeder
{
  public static class Seeder
  {
    public static async Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      await SeedRoles(roleManager);
      await SeedUsers(userManager, roleManager);
    }

    private static async Task SeedUsers(UserManager<IdentityUser>? userManager, RoleManager<IdentityRole> roleManager)
    {
      var adminUser = new IdentityUser()
      {
        UserName = "admin@admin.com",
        Email = "admin@admin.com",
        EmailConfirmed = true
      };
      string password = "Admin1!";
      var user = await userManager.FindByEmailAsync(adminUser.Email);
      if (user == null)
      {
        var created = await userManager.CreateAsync(adminUser, password);
        if (created.Succeeded)
        {
          await userManager.AddToRoleAsync(adminUser, "Admin");
        }
      }

      var regularUser = new IdentityUser()
      {
        UserName = "user@user.com",
        Email = "user@user.com",
        EmailConfirmed = true
      };
      var userPassword = "User1!";
      var regularUserExist = await userManager.FindByEmailAsync(regularUser.Email);
      if (regularUserExist == null)
      {
        var created = await userManager.CreateAsync(regularUser, userPassword);
        if (created.Succeeded)
        {
          await userManager.AddToRoleAsync(regularUser, "User");
        }
      }
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
      string[] roleNames = { "Admin", "User" };
      foreach (var roleName in roleNames)
      {
        bool roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
          await roleManager.CreateAsync(new IdentityRole(roleName));
        }
      }
    }
  }
}
