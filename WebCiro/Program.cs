using Microsoft.EntityFrameworkCore;
using WebCiro.Extenisons;
using WebCiro.Hubs;
using WebCiro.MiddlewareExtensions;
using WebCiro.Models;
using WebCiro.SalesTableDependencies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddSignalR();
builder.Services.AddDbContext<WebCiroDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentityWithExt();
//builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.ConfigureApplicationCookie(opt =>
{
    var cookieBuilder = new CookieBuilder();

    cookieBuilder.Name = "AsinTeknolojiWebCiro";
    opt.LoginPath = new PathString("/SignIn/Index");
    opt.LogoutPath = new PathString("/Member/logout");
    opt.AccessDeniedPath = new PathString("/Member/AccessDenied");
    opt.Cookie = cookieBuilder;
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true;
});

builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SalesTableDependencies>();
var app = builder.Build();
var connectionString = app.Configuration.GetConnectionString("DefaultConnection");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapHub<DashboardHub>("/dashboardHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.UseSqlTableDependency<SalesTableDependencies>(connectionString);

app.Run();