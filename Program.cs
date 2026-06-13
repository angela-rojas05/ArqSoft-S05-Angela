using CitasApp.Infrastructure.Repositories;
using CitasApp.Interfaces;
using CitasApp.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Carpeta de datos
var dataFolder = Path.Combine(builder.Environment.ContentRootPath, "data");

Directory.CreateDirectory(dataFolder);

// Archivos CSV
var csvPacientes = Path.Combine(dataFolder, "pacientes.csv");
var csvMedicos = Path.Combine(dataFolder, "medicos.csv");
var csvCitas = Path.Combine(dataFolder, "citas.csv");

// Repositorios CSV
/*builder.Services.AddScoped<IPacienteRepository>(
    _ => new CsvPacienteRepository(csvPacientes));

builder.Services.AddScoped<IMedicoRepository>(
    _ => new CsvMedicoRepository(csvMedicos));

builder.Services.AddScoped<ICitaRepository>(
    _ => new CsvCitaRepository(csvCitas)); */


// a Json
builder.Services.AddScoped<IPacienteRepository, JsonPacienteRepository>();
builder.Services.AddScoped<IMedicoRepository, JsonMedicoRepository>();
builder.Services.AddScoped<ICitaRepository, JsonCitaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();