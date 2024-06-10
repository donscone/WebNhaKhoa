using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NhaKhoaQuangVu.DataAccess;
using NhaKhoaQuangVu.Repositories;
using NhaKhoaQuangVu.Models;
using Microsoft.AspNetCore.Identity;
using NhaKhoaQuangVu.Helper;
using NhaKhoaQuangVu.Models.Momo;
using NhaKhoaQuangVu.Services;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddDefaultTokenProviders()
.AddDefaultUI()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<UserManager<ApplicationUser>>();

builder.Services.AddRazorPages();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IBangGiaRepository, EFBangGiaRepository>();
builder.Services.AddScoped<IEmployeeRepository, EFEmployeeRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<IDatHenRepository, EFDatHenRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");

        googleOptions.ClientId = googleAuthNSection["ClientId"];
        googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
        googleOptions.CallbackPath = "/dang-nhap-tu-google";
    });


var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowAllOrigins");
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapEmployeeDefaultAreaRoute();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Admin}/{action=CreateAdminAccount}/{id?}");

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=BangGia}/{action=Index}/{id?}");
app.MapControllerRoute(
name: "employee",
pattern: "{ area: exists}/{controller=Employee}/{action = Update}/{ id ?}"
    );
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
