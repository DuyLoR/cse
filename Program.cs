using CSE_website.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
using CSE_website.Services;
using CSE_website.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

// Load env
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Connection
var connectionString = builder.Configuration.GetConnectionString("CSEWebsiteContext");
connectionString = connectionString.Replace("$DATABASE_SERVER", Environment.GetEnvironmentVariable("DATABASE_SERVER"));
connectionString = connectionString.Replace("$DATABASE_PORT", Environment.GetEnvironmentVariable("DATABASE_PORT"));
connectionString = connectionString.Replace("$DATABASE_NAME", Environment.GetEnvironmentVariable("DATABASE_NAME"));
connectionString = connectionString.Replace("$DATABASE_USER", Environment.GetEnvironmentVariable("DATABASE_USER"));
connectionString = connectionString.Replace("$DATABASE_PASSWORD", Environment.GetEnvironmentVariable("DATABASE_PASSWORD"));
builder.Services.AddDbContext<CSEWebsiteContext>(options => options.UseMySQL(connectionString));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Login/Logout";
    options.AccessDeniedPath = "/Account/Login";
    options.Cookie.Name = "LoginCookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

// Self-definition services 
builder.Services.AddSingleton<IUploadFileService, UploadFileService>();
builder.Services.AddSingleton<ISendMailService, SendMailService>();

// Build configurations to start app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

/*phân quyền và bảo mật*/
app.UseAuthentication();

app.UseAuthorization();
/*chuyển hướng về Login mỗi khi người dùng không có quyền truy cập: access dinied*/
app.UseStatusCodePagesWithRedirects("/Admin/Login");
app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.MapRazorPages();
app.Run();