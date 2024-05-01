using Business;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using DataAccess;
using DataAccess.Contexts;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});



builder.Services.AddDataAccessRegistration();
builder.Services.AddBusinnesRegistration();



builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = new PathString("/auth/login");
    opt.AccessDeniedPath = new PathString("/auth/login");
    opt.SlidingExpiration = true;
    opt.ExpireTimeSpan = TimeSpan.FromSeconds(60);
});


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
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
