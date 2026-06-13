# Actividad #18 - Práctica .NET: Arquitectura Hexagonal en C#

## 📌 Datos institucionales

- **Universidad:** Tecnológico de Software
- **Materia:** Arquitectura de Software
- **Proyecto:** CitasApp en hexagonal 
- **Alumno:** Ángela Yaritzi Rojas Brito
- **Grupo:** 3B
- **Profesor:** Jorge Javier Pedrozo Romero
- **Fecha:** 09/06/26

---

# 📖 Descripción del proyecto

El Sistema de Citas Médicas es una aplicación web diseñada para facilitar la administración de pacientes, médicos y citas dentro de un consultorio o clínica.

El sistema permite registrar nuevos pacientes y médicos, programar citas médicas y consultar la información almacenada mediante una interfaz sencilla y organizada. Su objetivo es centralizar el control de las citas y mantener un registro básico de la información necesaria para la atención de los pacientes.

Para esta versión del proyecto se implementó una arquitectura basada en el patrón de Arquitectura Hexagonal, separando las responsabilidades en diferentes capas para mejorar la organización, mantenibilidad y escalabilidad del sistema. Esta separación permite desacoplar la lógica de negocio de la interfaz de usuario y de los mecanismos de persistencia de datos.


---

# 🚀 Tecnologías Utilizadas

Durante el desarrollo del proyecto se utilizaron las siguientes tecnologías y herramientas:

- ASP.NET Core MVC para la construcción de la aplicación web.
- C# como lenguaje principal de programación.
- Arquitectura Hexagonal para la organización del sistema.
- Razor Views (.cshtml) para la construcción de las interfaces de usuario.
- HTML5 para la estructura de las páginas web.
- CSS3 para el diseño y personalización de la interfaz gráfica.
- JSON como mecanismo de almacenamiento y persistencia de datos.
- Inyección de Dependencias (Dependency Injection).
- Repositorios e Interfaces para desacoplar el acceso a datos.
- Bibliotecas de clases (.NET Class Library) para la separación de capas.
- Visual Studio 2022 como entorno de desarrollo integrado.
- .NET 8 como plataforma de ejecución.
- Git para el control de versiones.
- GitHub para el alojamiento del repositorio.
- Bootstrap para elementos responsivos de la interfaz.


---

# 📌 Características

## Funcionales

- Registro de pacientes.
- Registro de médicos.
- Programación de citas médicas.
- Consulta de información de pacientes, médicos y citas.
- Visualización de registros mediante tablas.
- Persistencia de datos utilizando archivos JSON.

## Arquitectónicas

- Separación de responsabilidades mediante Arquitectura Hexagonal.
- Uso de interfaces para definir contratos de acceso a datos.
- Implementación de repositorios para la gestión de información.
- Desacoplamiento entre la lógica de negocio y la infraestructura.
- Uso de inyección de dependencias para la resolución de servicios.
- Organización modular mediante proyectos independientes.

---

# ▶️ ¿Cómo funciona?

La aplicación se encuentra dividida en cuatro proyectos principales que colaboran entre sí para atender las solicitudes del usuario.

## Flujo general

1. El usuario interactúa con la interfaz web.
2. Los controladores reciben las solicitudes desde la capa Web.
3. La capa Application coordina la lógica de aplicación y los casos de uso.
4. La capa Domain define las entidades y contratos necesarios para operar.
5. La capa Infrastructure implementa dichos contratos mediante repositorios.
6. Los repositorios leen y actualizan la información almacenada en archivos JSON.
7. Los resultados regresan hasta la interfaz para ser mostrados al usuario.


---

## Flujo de capas

```text
CitasApp.Web
      │
      ▼
CitasApp.Application
      │
      ▼
CitasApp.Domain
      ▲
      │
CitasApp.Infrastructure
      │
      ▼
 Archivos JSON
```

Esta estructura permite modificar la forma de almacenamiento de datos o la interfaz de usuario sin afectar las reglas principales del negocio.

---

## 🎨 Diseño y navegación
Todas las vistas comparten una plantilla común que mantiene una navegación uniforme y una apariencia consistente en toda la aplicación.

### Archivos involucrados:
* `Views/Shared/_Layout.cshtml`
* `wwwroot/css/site.css`
* `wwwroot/js/site.js`

---

# 📁 Estructura del proyecto

```text
CitasApp.sln
│
├── CitasApp.Domain
│   │
│   ├── Models
│   │   ├── Paciente.cs
│   │   ├── Medico.cs
│   │   ├── Cita.cs
│   │   └── CitaJson.cs
│   │
│   └── Interfaces
│       ├── IPacienteRepository.cs
│       ├── IMedicoRepository.cs
│       └── ICitaRepository.cs
│
├── CitasApp.Application
│   │
│   └── Services
│       ├── PacienteService.cs
│       ├── MedicoService.cs
│       └── CitaService.cs
│
├── CitasApp.Infrastructure
│   │
│   └── Repositories
│       ├── JsonPacienteRepository.cs
│       ├── JsonMedicoRepository.cs
│       └── JsonCitaRepository.cs
│
└── CitasApp.Web
    │
    ├── Controllers
    │   ├── HomeController.cs
    │   ├── PacienteController.cs
    │   ├── MedicoController.cs
    │   └── CitaController.cs
    │
    ├── Views
    │   ├── Home
    │   ├── Paciente
    │   ├── Medico
    │   ├── Cita
    │   └── Shared
    │
    ├── data
    │   ├── pacientes.json
    │   ├── medicos.json
    │   └── citas.json
    │
    ├── wwwroot
    │   ├── css
    │   ├── js
    │   └── lib
    │
    ├── Program.cs
    └── appsettings.json
```
---

## Descripción de las capas

### CitasApp.Domain

La capa **Domain** contiene las entidades principales del sistema y las interfaces que definen los contratos para el acceso a los datos. Aquí se encuentran los modelos de negocio como Paciente, Médico y Cita, así como las interfaces de los repositorios. Esta capa representa las reglas fundamentales del sistema y no depende de ninguna otra capa, lo que permite mantener la lógica del dominio independiente de tecnologías específicas.

### CitasApp.Application

La capa **Application** contiene los servicios y casos de uso de la aplicación. Su función es coordinar la lógica necesaria para atender las solicitudes realizadas por los usuarios, actuando como intermediaria entre la interfaz web y el dominio. Esta capa organiza los procesos de negocio sin preocuparse por detalles de almacenamiento o presentación.

### CitasApp.Infrastructure

La capa **Infrastructure** contiene las implementaciones concretas de los contratos definidos en el dominio. En este proyecto se encarga de la persistencia de los datos mediante archivos JSON, utilizando repositorios especializados para la lectura y escritura de la información de pacientes, médicos y citas.

### CitasApp.Web

La capa **Web** corresponde a la interfaz de usuario desarrollada con ASP.NET Core MVC. Incluye los controladores, vistas, recursos estáticos y configuraciones necesarias para que los usuarios interactúen con el sistema. Esta capa recibe las solicitudes del usuario y muestra la información procesada por las demás capas.

---

# 📷 Capturas de pantalla


![Home](img/Home.png)

![Privacy](img/Privacy.png)

![Cita](img/Cita.png)

![Agregar Cita](img/AgCita.png)

![Médico](img/Medico.png)

![Agregar Médico](img/AgMedico.png)

![Paciente](img/Paciente.png)

![Agregar Paciente](img/AgPaciente.png)

---

# 🤖 Cláusula de IA

En este proyecto se utilizaron herramientas de Inteligencia Artificial (IA) como apoyo técnico y de consulta. 

La IA se usó específicamente para:

- Datos: Ayuda con la lectura y sincronización de archivos JSON.

- Controladores: Soporte para conectar los controladores con el almacenamiento.

- Diseño: Sugerencias de estilos CSS y diseño visual.

- Vistas: Apoyo para crear los formularios y botones de pacientes, médicos y citas.

---

# 📂 Contacto

- Hecho por: Ángela Yaritzi Rojas Brito
- Correo: angela.rojas@tecdesoftware.edu.mx


