using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Service;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();



        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Proyecto de Prácticas",
                Version = "v1"
            });

            // ✅ Configuración de seguridad JWT para Swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Introduce el token JWT con el prefijo Bearer. Ejemplo: \"Bearer {tu_token}\""
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
        });




        // ✅ Configuración de JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],   // desde appsettings.json
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                    )
                };
            });

        builder.Services.AddAuthorization();

        // Base de datos
        builder.Services.AddDbContext<AplicationDBContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
        });

        // AutoMapper
        builder.Services.AddAutoMapper(typeof(Program));

        // Servicios
        builder.Services.AddScoped<ILaboratoriosRepository, LaboratoriosRepository>();
        builder.Services.AddScoped<ILaboratoriosService, LaboratoriosService>();

        // Aulas
        builder.Services.AddScoped<IAulasRepository, AulaRepository>();
        builder.Services.AddScoped<IAulasService, AulasServie>();
            
        //Usuarios 
        builder.Services.AddScoped<IUsuariosRepository, UsuarioRepository>();
        builder.Services.AddScoped<IUsuariosServices, UsuariosService>();

        // Roles
        builder.Services.AddScoped<IRolesRepository, RolesRepository>();
        builder.Services.AddScoped<IRolesService, RolesService>();
        builder.Services.AddScoped<IEquiposRepository, EquiposRepository>();

        builder.Services.AddScoped<IEquiposService, EquiposService>();

        builder.Services.AddScoped<IPisosRepository, PisosRepository>();
        builder.Services.AddScoped<IPisosService, PisosService>();

        builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();
        builder.Services.AddScoped<ICategoriasService, CategoriasService>();

        builder.Services.AddScoped<IFacultadesRepository, FacultadesRepository>();
        builder.Services.AddScoped<IFacultadesService, FacultadesService>();

        builder.Services.AddScoped<IUsuarioFacultadRolService, UsuarioFacultadRolService>();

        builder.Services.AddAutoMapper(typeof(Program));
        
        builder.Services.AddScoped<IUsuarioFacultadRolRepository, UsuarioFacultadRolRepository>();

        // Herramientas
        var app = builder.Build(); 
            
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        // ✅ IMPORTANTE: primero Authentication, luego Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}