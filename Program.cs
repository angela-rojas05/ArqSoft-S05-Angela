using CitasApp.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
Directory.CreateDirectory(dataFolder);

// rutas CSV
var csvPacientes = Path.Combine(dataFolder, "pacientes.csv");
var csvMedicos = Path.Combine(dataFolder, "medicos.csv");
var csvCitas = Path.Combine(dataFolder, "citas.csv");

// rutas SQLite
var sqlitePath = Path.Combine(dataFolder, "citasapp.db");

// JSON
//builder.Services.AddSingleton<IPacienteRepository, JsonPacienteRepository>();
//builder.Services.AddSingleton<IMedicoRepository, JsonMedicoRepository>();
//builder.Services.AddSingleton<ICitaRepository, JsonCitaRepository>();

// CSV
builder.Services.AddSingleton<IPacienteRepository>(_ => new CsvPacienteRepository(csvPacientes));
builder.Services.AddSingleton<IMedicoRepository>(_ => new CsvMedicoRepository(csvMedicos));
builder.Services.AddSingleton<ICitaRepository>(_ => new CsvCitaRepository(csvCitas));

// SQLite
// builder.Services.AddSingleton<IPacienteRepository>(_ => new SqlitePacienteRepository(sqlitePath));
// builder.Services.AddSingleton<IMedicoRepository>(_ => new SqliteMedicoRepository(sqlitePath));
// builder.Services.AddSingleton<ICitaRepository>(_ => new SqliteCitaRepository(sqlitePath));

var app = builder.Build();

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