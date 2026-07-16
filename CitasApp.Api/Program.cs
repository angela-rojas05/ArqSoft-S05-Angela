using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Infrastructure.Observers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Repositorios (registrados directo con DI, sin fábrica intermedia)
if (builder.Environment.IsProduction())
{
    builder.Services.AddScoped<IPacienteRepository>(sp =>
        new LoggingPacienteRepository(new MemoriaPacienteRepository()));
}
else
{
    builder.Services.AddScoped<IPacienteRepository>(sp =>
        new LoggingPacienteRepository(new JsonPacienteRepository()));
}

builder.Services.AddScoped<IMedicoRepository, JsonMedicoRepository>();
builder.Services.AddScoped<ICitaRepository, JsonCitaRepository>();

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