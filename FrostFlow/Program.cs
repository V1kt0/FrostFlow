using FrostFlow.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DreamsAI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
      builder.Services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(connectionString));

      builder.Services.AddDatabaseDeveloperPageExceptionFilter();

      // Add default Identity services
      builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
          .AddEntityFrameworkStores<ApplicationDbContext>();

      builder.Services.AddControllersWithViews();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
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

      // Apply [Authorize] globally to require login
      app.UseEndpoints(endpoints =>
      {
        // This will apply the [Authorize] filter globally to all controllers and actions
        endpoints.MapControllers().RequireAuthorization();
        // Allow anonymous access to the Login and Register actions
        endpoints.MapControllerRoute(
            name: "login",
            pattern: "Account/Login",
            defaults: new { controller = "Account", action = "Login" });

        endpoints.MapControllerRoute(
            name: "register",
            pattern: "Account/Register",
            defaults: new { controller = "Account", action = "Register" });
      });

      // Define routes and map controller routes
      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
      app.MapRazorPages();

      app.Run();
    }
  }
}
