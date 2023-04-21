using Client.Business;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ManageContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ManageDb")).UseLazyLoadingProxies());
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<DbContext, ManageContext>();
builder.Services.AddScoped<TokenExpirationFilter>();
builder.Services.AddAccessTokenManagement();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://localhost:44367";
    options.ClientId = "client_id_mvc_1";
    options.ClientSecret = "client_xuanhuongvanhungnguoiban_1";
    options.RequireHttpsMetadata = false;
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.SignedOutCallbackPath = "/Home";
    // configure scope
    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("offline_access");
    options.Scope.Add("profile");
    options.Scope.Add("email");
});
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
