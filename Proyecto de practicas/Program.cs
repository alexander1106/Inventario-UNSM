using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Modules.Articulos.Repository;
using Proyecto_de_practicas.Modules.Articulos.Repository.IArticulosRepository;
using Proyecto_de_practicas.Modules.Articulos.Services;
using Proyecto_de_practicas.Modules.Mantenimiento.Service;
using Proyecto_de_practicas.Modules.Mantenimiento.Service.IService;
using Proyecto_de_practicas.Modules.Prestamos.Services;
using Proyecto_de_practicas.Modules.Prestamos.Services.IServices;
using Proyecto_de_practicas.Modules.Reportes.Repository;
using Proyecto_de_practicas.Modules.Reportes.Repository.IReporteRepository;
using Proyecto_de_practicas.Modules.Reportes.Services;
using Proyecto_de_practicas.Modules.Reportes.Services.IReporteService;
using Proyecto_de_practicas.Modules.Security.Entities;
using Proyecto_de_practicas.Modules.Security.Repositories;
using Proyecto_de_practicas.Modules.Security.Repositories.IRepositories;
using Proyecto_de_practicas.Modules.Security.Services;
using Proyecto_de_practicas.Modules.Security.Services.IServices;
using Proyecto_de_practicas.Modules.Traslados.Repository;
using Proyecto_de_practicas.Modules.Traslados.Repository.IRespository;
using Proyecto_de_practicas.Modules.Traslados.Service;
using Proyecto_de_practicas.Modules.Traslados.Service.IService;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository;
using Proyecto_de_practicas.Modules.Ubicaciones.Repository.IUbicacionesRepository;
using Proyecto_de_practicas.Modules.Ubicaciones.Services;
using Proyecto_de_practicas.Modules.Ubicaciones.Services.IUbicacionesServices;
using Proyecto_de_practicas.Service;

// 🔥 IMPORTANTE
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {

        // 🔥 CONFIGURACIÓN DE SERILOG (ANTES DE BUILDER)
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(
                "logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"
            )
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        // 🔥 CONECTAR SERILOG
        builder.Host.UseSerilog();

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
        builder.Services.AddScoped<IUsuariosRepository, UsuarioRepository>();
        builder.Services.AddScoped<IUsuariosServices, UsuariosService>();

        builder.Services.AddScoped<ITrasladoRepository, TrasladoRepository>();
        builder.Services.AddScoped<ITrasladoService, TrasladoService>();

        builder.Services.AddScoped<IRolesRepository, RolesRepository>();
        builder.Services.AddScoped<IRolesService, RolesService>();

        builder.Services.AddScoped<ITipoArticuloRepository, TipoArticuloRepository>();
        builder.Services.AddScoped<ITipoArticuloService, TipoArticuloService>();

        builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();
        builder.Services.AddScoped<IUbicacionService, UbicacionService>();

        builder.Services.AddScoped<IArticuloRepository, ArticuloRepository>();
        builder.Services.AddScoped<IArticuloService, ArticuloService>();

        builder.Services.AddScoped<ICampoArticuloRepository, CampoArticuloRepository>();
        builder.Services.AddScoped<ICampoArticuloService, CampoArticuloService>();

        builder.Services.AddScoped<IArticuloCampoValorRepository, ArticuloCampoValorRepository>();
        builder.Services.AddScoped<IArticuloCampoValorService, ArticuloCampoValorService>();

        builder.Services.AddScoped<ITipoUbicacionRepository, TipoUbicacionRepository>();
        builder.Services.AddScoped<ITipoUbicacionService, TipoUbicacionService>();

        builder.Services.AddScoped<IModulosRepository, ModulosRepository>();
        builder.Services.AddScoped<IModulosService, ModulosService>();

        builder.Services.AddScoped<ISubModulosRepository, SubModulosRepository>();
        builder.Services.AddScoped<ISubModulosService, SubModulosService>();

        builder.Services.AddScoped<IRolPermisosService, RolPermisosService>();

        builder.Services.AddScoped<IRolPermisoRepository, RolPermisosRepository>();

        builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
        builder.Services.AddScoped<IPermisoService, PermisoService>();

        builder.Services.AddScoped<IReportesRepository, ReportesRepository>();
        builder.Services.AddScoped<IReportesService, ReportesService>();
        builder.Services.AddScoped<IServicePrestamos, PrestamoService>();
        builder.Services.AddScoped<IMantenimeintosService, MantenimeintosService>();

        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();
        app.UseMiddleware<ErrorHandlerMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();

        var imagenesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");

        if (!Directory.Exists(imagenesPath))
        {
            Directory.CreateDirectory(imagenesPath);
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(imagenesPath),
            RequestPath = "/imagenes"
        });

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}