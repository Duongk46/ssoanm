using Entities.Entities;
using IdentityServer.Configure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<DbContext, ManageContext>();
builder.Services.AddDbContext<ManageContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ManageDb")).UseLazyLoadingProxies());
builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireUppercase = false;
    config.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<ManageContext>().AddDefaultTokenProviders();
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryClients(ConfigurationIS4.GetClients())
    .AddInMemoryIdentityResources(ConfigurationIS4.GetIdentityResources())
    .AddInMemoryApiResources(ConfigurationIS4.GetApis())
    .AddDeveloperSigningCredential();
builder.Services.AddControllersWithViews();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
