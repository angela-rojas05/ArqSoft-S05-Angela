# Componentes de CitasApp


**Objetivo**

Este diagrama muestra el flujo completo de CitasApp, desde la interacción del usuario hasta la persistencia de los datos, incluyendo la evolución hacia una Arquitectura Hexagonal y la incorporación de los patrones GOF Factory y Decorator.

---

```mermaid
flowchart TD

Client[Usuario / Navegador]

subgraph WEB["CitasApp.Web"]
    Views[Views Razor]
    Controllers[Controllers MVC]
    Program[Program.cs<br/>Inyección de Dependencias]
end

subgraph APP["CitasApp.Application"]
    PS[PacienteService]
    MS[MedicoService]
    CS[CitaService]
end

subgraph DOMAIN["CitasApp.Domain"]
    Models[Models<br/>Paciente<br/>Medico<br/>Cita]

    Ports[Interfaces<br/>IPacienteRepository<br/>IMedicoRepository<br/>ICitaRepository]
end

subgraph INFRA["CitasApp.Infrastructure"]

    Json[Repositorios JSON<br/>JsonPacienteRepository<br/>JsonMedicoRepository<br/>JsonCitaRepository]

    Csv[Repositorios CSV<br/>CsvPacienteRepository<br/>CsvMedicoRepository<br/>CsvCitaRepository]

    Sqlite[Repositorios SQLite<br/>SqlitePacienteRepository<br/>SqliteMedicoRepository<br/>SqliteCitaRepository]

    Memory[MemoriaPacienteRepository]

    Factory[RepositoryFactory<br/>Factory Pattern]

    Decorator[LoggingPacienteRepository<br/>Decorator Pattern]
end

subgraph DATA["Persistencia"]
    JsonFiles[(pacientes.json<br/>medicos.json<br/>citas.json)]
    CsvFiles[(pacientes.csv<br/>medicos.csv<br/>citas.csv)]
    Db[(SQLite)]
end

Client --> Views
Views --> Controllers
Controllers --> Program

Program --> PS
Program --> MS
Program --> CS

PS --> Ports
MS --> Ports
CS --> Ports

Ports --> Factory
Factory --> Json
Factory --> Csv
Factory --> Sqlite
Factory --> Memory

Ports --> Decorator
Decorator --> Json

Json --> JsonFiles
Csv --> CsvFiles
Sqlite --> Db

Models -. Entidades del dominio .-> Ports
```