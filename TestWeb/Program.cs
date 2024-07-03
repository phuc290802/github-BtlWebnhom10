using Microsoft.EntityFrameworkCore;
using TestWeb;
using TestWeb.Models;
using TestWeb.Repository;

var builder = WebApplication.CreateBuilder(args);
//add gmail

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<QuanLiBanSachContext>();
var connectionString = builder.Configuration.GetConnectionString("QuanLiBanSachContext");
builder.Services.AddDbContext<QuanLiBanSachContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<IloaispRepository, loaispRepository>();

builder.Services.AddSession();

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

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
