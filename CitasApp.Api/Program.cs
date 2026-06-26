using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Infrastructure.Observers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Repositorios
builder.Services.AddScoped<IPacienteRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();

    var repo = RepositoryFactory.CrearPacienteRepository(
        builder.Environment.EnvironmentName, env);

    return new LoggingPacienteRepository(repo);
});

builder.Services.AddScoped<IMedicoRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();

    return RepositoryFactory.CrearMedicoRepository(
        builder.Environment.EnvironmentName, env);
});

builder.Services.AddScoped<ICitaRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();

    return RepositoryFactory.CrearCitaRepository(
        builder.Environment.EnvironmentName, env);
});

// Observers
builder.Services.AddScoped<ICitaObserver, SmsObserver>();
builder.Services.AddScoped<ICitaObserver, EmailObserver>();

// Servicios de aplicación
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();