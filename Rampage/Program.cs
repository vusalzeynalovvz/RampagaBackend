using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rampage.Areas.Admin.Utilities.Helpers;
using Rampage.Areas.Admin.Utilities.Services;
using Rampage.Database;
using Rampage.Database.DomainModels;
using Rampage.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<MailKitHelper>();
builder.Services.AddScoped<LayoutService>();

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;


    opt.SignIn.RequireConfirmedEmail = true;

    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(3);
    opt.Lockout.MaxFailedAccessAttempts = 3;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
