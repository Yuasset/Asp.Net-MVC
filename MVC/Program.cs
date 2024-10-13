using Microsoft.EntityFrameworkCore;
using MVC.Models.Identity;
using MVC.Models.Market;
using MVC.Repositories.Abstracts;
using MVC.Repostory.Concretes;
using MVC.Services.Abstracts;
using MVC.Services.Concretes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //MCV Eklendi
builder.Services.AddDbContext<MarketContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //ConnectionString Eklendi

//Identity Service
builder.Services.AddIdentity<AppUser, AppUserRole>().AddEntityFrameworkStores<MarketContext>();

//Instances (Nesneler) Dependency Injection ile oluþturmak
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IServiceManager<>), typeof(ServiceManager<>));

//Custom Service
builder.Services.AddScoped<IProductServiceManager, ProductServiceManager>();
builder.Services.AddScoped<ICategoryServiceManager, CategoryServiceManager>();

//Login Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Account.Cookie";
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = System.TimeSpan.FromMinutes(60);
});

//Session Cookie - Kullanýcýnýn sepet bilgilerini tutmak için kullanýlacak.
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Account.Session";
    options.IdleTimeout = System.TimeSpan.FromMinutes(10);
});


var app = builder.Build();

app.UseSession();

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
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});




app.Run();
