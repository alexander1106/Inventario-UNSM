using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Models;
using Proyecto_de_practicas.Modules.Articulos.Repository;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;
using Proyecto_de_practicas.Modules.Articulos.Services;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Services;
using Proyecto_de_practicas.Modules.Security.Services.IServices;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;
using Proyecto_de_practicas.Modules.Ubicaciones.Services;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;
using Proyecto_de_practicas.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ========================
        //        CONTROLLERS
        // ========================
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // ========================
        //        SWAGGER + JWT
        // ========================
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Proyecto de Prácticas",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Introduce el token con Bearer. Ej: Bearer {token}"
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

        // ========================
        //           CORS
        // ========================
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policy => policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
        });

        // ========================
        //           JWT
        // ========================
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                    )
                };
            });

        builder.Services.AddAuthorization();

        // ========================
        //       BASE DE DATOS
        // ========================
        builder.Services.AddDbContext<AplicationDBContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
        });

        // ========================
        //        AUTOMAPPER
        // ========================
        builder.Services.AddAutoMapper(typeof(Program));

        // ========================
        //     REPOS & SERVICES
        // ========================

        // Usuarios
        builder.Services.AddScoped<IUsuariosRepository, UsuarioRepository>();
        builder.Services.AddScoped<IUsuariosServices, UsuariosService>();

        // Roles
        builder.Services.AddScoped<IRolesRepository, RolesRepository>();
        builder.Services.AddScoped<IRolesService, RolesService>();

        // Tipo Artículo
        builder.Services.AddScoped<ITipoArticuloRepository, TipoArticuloRepository>();
        builder.Services.AddScoped<ITipoArticuloService, TipoArticuloService>();

        // Ubicaciones
        builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();
        builder.Services.AddScoped<IUbicacionService, UbicacionService>();

        // Artículos
        builder.Services.AddScoped<IArticuloRepository, ArticuloRepository>();
        builder.Services.AddScoped<IArticuloService, ArticuloService>();

        // Campos Artículos
        builder.Services.AddScoped<ICampoArticuloRepository, CampoArticuloRepository>();
        builder.Services.AddScoped<ICampoArticuloService, CampoArticuloService>();

        // Articulos - CampoValor
        builder.Services.AddScoped<IArticuloCampoValorRepository, ArticuloCampoValorRepository>();
        builder.Services.AddScoped<IArticuloCampoValorService, ArticuloCampoValorService>();

        // Tipo Ubicación
        builder.Services.AddScoped<ITipoUbicacionRepository, TipoUbicacionRepository>();
        builder.Services.AddScoped<ITipoUbicacionService, TipoUbicacionService>();

        // Módulos
        builder.Services.AddScoped<IModulosRepository, ModulosRepository>();
        builder.Services.AddScoped<IModulosService, ModulosService>();

        // SubMódulos
        builder.Services.AddScoped<ISubModulosRepository, SubModulosRepository>();
        builder.Services.AddScoped<ISubModulosService, SubModulosService>();

        // Rol - SubMódulo
        builder.Services.AddScoped<IRolSubModuloRepository, RolSubModuloRepository>();
        builder.Services.AddScoped<IRolSubModuloService, RolSubModuloService>();

        // Permisos
        builder.Services.AddScoped<IRolSubModuloPermisoRepository, RolSubModuloPermisoRepository>();
        builder.Services.AddScoped<IRolSubModuloPermisoService, RolSubModuloPermisoService>();

        builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
        builder.Services.AddScoped<IPermisoService, PermisoService>();

        // ========================
        //      APP BUILD
        // ========================
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Archivos estáticos (wwwroot)
        app.UseStaticFiles();

        // CORS antes de auth
        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
