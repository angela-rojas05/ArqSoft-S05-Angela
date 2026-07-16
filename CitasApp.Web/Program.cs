using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Data;
using CitasApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

Directory.SetCurrentDirectory(builder.Environment.ContentRootPath);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CitasAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CitasAppConnection")));

builder.Services.AddScoped<IPacienteRepository, PostgresPacienteRepository>();
builder.Services.AddScoped<IMedicoRepository, PostgresMedicoRepository>();
builder.Services.AddScoped<ICitaRepository, PostgresCitaRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/");
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