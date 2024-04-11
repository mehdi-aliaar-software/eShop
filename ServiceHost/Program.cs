using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using BM.Infrastructure.Configuration;
using DM.Configuration;
using IM.Infrastructure.Configuration;
using ServiceHost;
using SM.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string conn = builder.Configuration.GetConnectionString("ShopDb");
ShopManagementBootstrapper.Configure(builder.Services, conn);

string discountConn = builder.Configuration.GetConnectionString("DiscountDb");
DiscountManagementBootstrapper.Configure(builder.Services, discountConn);

string inventoryConn = builder.Configuration.GetConnectionString("InventoryDb");
InventoryManagementBootstrapper.Configure(builder.Services, inventoryConn);

string blogConn = builder.Configuration.GetConnectionString("BlogDb");
BlogManagementrBootstrapper.Configure(builder.Services, blogConn);
builder.Services.AddTransient<IFileUploader, FileUploader>();


builder.Services.AddSingleton(
    HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
