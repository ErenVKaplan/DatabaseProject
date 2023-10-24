using DatabaseProject.Entities;
using DatabaseProject.Helpers;
using DatabaseProject.Services.Abstract;
using DatabaseProject.Services.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.Cookie.Name = "Giris";
    options.LoginPath = "/Login/Login";
    options.AccessDeniedPath = "/Home/Index";//Yetki icin
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});
builder.Services.AddSingleton<IHelper, Helper>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<KullaniciService>();
builder.Services.AddScoped<BankService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
app.UseCookiePolicy();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Register}/{id?}");

app.Run();
