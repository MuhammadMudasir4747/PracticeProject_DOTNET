using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Optivem.Framework.Core.Domain;
using PracticeProject.DataAccess.Repository;
using PracticeProject.DataAccess.Repository.IRepository;
using PracticeProject_DOTNET.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Authentication & Authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Customer/Account/Login";
        options.AccessDeniedPath = "/Customer/Account/Login";
    });

builder.Services.AddAuthorization();

// ✅ Add Services
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<MyNewDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PracticeProject.DataAccess.Repository.IRepository.IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// ✅ Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ IMPORTANT: Add authentication before authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Account}/{action=Signup}/{id?}");

app.Run();
