using FrostFlow.Data;
using FrostFlow.Data.Seeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FrostFlow
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container
      var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                             ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
      builder.Services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(connectionString));

      builder.Services.AddDatabaseDeveloperPageExceptionFilter();

      builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
          .AddRoles<IdentityRole>() // Add roles to Identity
          .AddEntityFrameworkStores<ApplicationDbContext>();

      builder.Services.AddControllersWithViews();

      var app = builder.Build();

      // Seed roles and users during app startup
      using (var scope = app.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
          var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
          await Seeder.Seed(userManager, roleManager); // Call the seed method
        }
        catch (Exception ex)
        {
          Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
        }
      }

      // Configure the HTTP request pipeline
      if (app.Environment.IsDevelopment())
      {
        app.UseMigrationsEndPoint();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();  // Authentication middleware
      app.UseAuthorization();   // Authorization middleware

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
      app.MapRazorPages();

      app.Run();
    }
  }
}
