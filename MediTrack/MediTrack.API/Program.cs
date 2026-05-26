using System.Text;
using MediTrack.Aplicacion.Interfaces;
using MediTrack.Aplicacion.Servicios;
using MediTrack.Infraestructura.Datos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Base de datos
builder.Services.AddDbContext<ContextoAplicacion>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionPrincipal")));

// Servicios
builder.Services.AddScoped<IServicioAutenticacion, ServicioAutenticacion>();
builder.Services.AddScoped<IServicioCitas, ServicioCitas>();
builder.Services.AddScoped<IServicioSedes, ServicioSedes>();
builder.Services.AddScoped<IServicioEspecialidades, ServicioEspecialidades>();
builder.Services.AddScoped<IServicioDoctores, ServicioDoctores>();

// JWT
var clave = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Clave"]!);

builder.Services.AddAuthentication(opciones =>
{
    opciones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opciones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opciones =>
{
    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Emisor"],
        ValidAudience = builder.Configuration["Jwt:Audiencia"],
        IssuerSigningKey = new SymmetricSecurityKey(clave)
    };
});



// Add services to the container.
builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("PermitirAngular", politica =>
    {
        politica.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger con soporte para JWT
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MediTrack API",
        Version = "v1"
    });

    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa tu token así: Bearer {token}"
    });

    opciones.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PermitirAngular");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Ejecutar seeder al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var contexto = scope.ServiceProvider.GetRequiredService<ContextoAplicacion>();
    await contexto.Database.MigrateAsync(); // Crea la BD si no existe
    await SeederDatos.EjecutarAsync(contexto);
}

app.Run();
