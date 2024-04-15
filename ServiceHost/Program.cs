using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AM.Configuration;
using BM.Infrastructure.Configuration;
using DM.Configuration;
using IM.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceHost;
using SM.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

string conn = builder.Configuration.GetConnectionString("ShopDb");
ShopManagementBootstrapper.Configure(builder.Services, conn);

string discountConn = builder.Configuration.GetConnectionString("DiscountDb");
DiscountManagementBootstrapper.Configure(builder.Services, discountConn);

string inventoryConn = builder.Configuration.GetConnectionString("InventoryDb");
InventoryManagementBootstrapper.Configure(builder.Services, inventoryConn);

string blogConn = builder.Configuration.GetConnectionString("BlogDb");
BlogManagementrBootstrapper.Configure(builder.Services, blogConn);
builder.Services.AddTransient<IFileUploader, FileUploader>();

string commentConn = builder.Configuration.GetConnectionString("CommentDb");
CommentManagementBootstrapper.Configure(builder.Services, commentConn);

string accountConn = builder.Configuration.GetConnectionString("AccountDb");
AccountManagementBootstrapper.Configure(builder.Services, accountConn);

builder.Services.AddSingleton(
    HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IFileUploader, FileUploader>();
builder.Services.AddSingleton<IAuthHelper, AuthHelper>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        p =>
        {
            p.LoginPath = new PathString("/Account");
            p.LogoutPath = new PathString("/Account");
            p.AccessDeniedPath = new PathString("/AccessDenied");
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea", 
        builder => builder.RequireRole(new List<string>
        {
            Roles.SystemAdmin, Roles.ContentUploader, Roles.SystemTechManager, Roles.SystemAnalyst
        }));

    options.AddPolicy("Shop",
        builder => builder.RequireRole(new List<string>
        {
            Roles.SystemAdmin
        }));

    options.AddPolicy("Discount",
        builder => builder.RequireRole(new List<string>
        {
            Roles.SystemAdmin, Roles.SystemAnalyst
        }));
    options.AddPolicy("Account",
        builder => builder.RequireRole(new List<string>
        {
            Roles.SystemAdmin, Roles.SystemAnalyst
        }));

});

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Discount");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
    });
        



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseCookiePolicy();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
