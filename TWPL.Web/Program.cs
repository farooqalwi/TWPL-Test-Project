using Microsoft.EntityFrameworkCore;
using TWPL.Core.Configurations;
using TWPL.Core.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add Db
var connectionString = builder.Configuration.GetConnectionString("FleetDb");
builder.Services.AddDbContext<FleetDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRepositoryServices();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
