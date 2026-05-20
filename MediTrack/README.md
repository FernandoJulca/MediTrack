# MediTrack
Sistema de Gestión Clínica e Inventario Médico desarrollado en ASP.NET Core.

## Tecnologías
- C# / ASP.NET Core 8 Web API
- Entity Framework Core 8
- SQL Server
- JWT Authentication
- Swagger

## Cómo ejecutar el proyecto

### Requisitos
- .NET 8 SDK
- SQL Server (local o Express)
- Visual Studio 2022

### Pasos
1. Clona el repositorio
   git clone https://github.com/tuusuario/MediTrack.git

2. Abre MediTrack.sln en Visual Studio 2022

3. Edita la cadena de conexión en MediTrack.API/appsettings.json
   "ConexionPrincipal": "Server=TU_SERVIDOR;Database=MediTrackDB;Trusted_Connection=True;TrustServerCertificate=True;"

4. Ejecuta el proyecto con F5
   - La base de datos se crea automáticamente
   - Los datos de prueba se insertan solos

## Usuarios de prueba
| Rol | Correo | Contraseña |
|---|---|---|
| Administrador | admin@meditrack.com | 123 |
| Recepcionista | recepcion@meditrack.com | 123 |
| Doctor | doctor1@meditrack.com | 123 |
| Doctor | doctor2@meditrack.com | 123 |
| Paciente | paciente1@meditrack.com | 123 |
| Paciente | paciente2@meditrack.com | 123 |

## Endpoints principales
- POST /api/Autenticacion/registrar
- POST /api/Autenticacion/login
- GET  /api/Inventario
- GET  /api/Inventario/stock-bajo
- GET  /api/Citas
- POST /api/Citas
- GET  /api/Ventas
- POST /api/Ventas