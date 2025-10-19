# CleanGo

CleanGo es un mercado para servicios de limpieza, conectando clientes y limpiadores profesionales.  
Construido con **.NET 9**, **PostgreSQL**, **React + TypeScript**, **Docker**, y siguiendo los **principios de Clean Architecture**.

## Features

- Autenticación de usuarios con JWT
- Búsqueda y reserva de servicios de limpieza
- Perfiles de limpiadores profesionales
- Gestión de programación y disponibilidad
- Reseñas y calificaciones

## Tech Stack

- **Backend:** .NET 9, C#, ASP.NET Core, EF Core  
- **Frontend:** React + TypeScript  
- **Base de Datos:** PostgreSQL  
- **Contenerización:** Docker, Docker Compose  
- **Nube (opcional):** Azure  

## Project Structure


CleanGo/
├── src/
│ ├── CleanGo.API/ # Controllers, Program.cs, Startup
│ ├── CleanGo.Application/ # Use cases, services, DTOs
│ ├── CleanGo.Domain/ # Entities, interfaces, business logic
│ └── CleanGo.Infrastructure/# Repositories, EF Core DbContext
├── tests/ # Unit and integration tests
├── docs/ # Architecture and API documentation
└── CleanGo.sln # Solution file.


## Getting Started

1. **Clone the repository:**

    ```bash
    git clone https://github.com/ebd1004alu/CleanGo.git
    ```

2. **Build and run with Docker Compose:**

    ```bash
    docker-compose up --build
    ```

Accede a la API en `http://localhost:5000` y al frontend en `http://localhost:3000`.

## Contributing

- Seguir los principios de Clean Architecture
- Utilizar ramas de características para nuevas tareas
- Escribir pruebas unitarias e de integración para toda nueva funcionalidad
