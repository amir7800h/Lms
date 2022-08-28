using Lms.Context;
using Lms.Context.IdentityDbContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddIdentityService();
builder.Services.AddAuthorization();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    option.LoginPath = "/Authentication/signin";
    option.AccessDeniedPath = "/Authentication/AccessDenied";
    option.SlidingExpiration = true;
});


builder.Services.AddDbContext<DataBaseContext>(p => p.UseSqlServer("Data Source=.;Initial Catalog=LmsDB;Integrated Security=True"));





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
    pattern: "{controller=authentication}/{action=signin}/{id?}");

app.Run();
