using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Data;
using Proyecto_de_practicas.Repository;
using Proyecto_de_practicas.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();



        builder.Services.AddDbContext<AplicationDBContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
        });

        //AutoMapper
        builder.Services.AddAutoMapper(typeof(Program));

        //Add service
        builder.Services.AddScoped<ILaboratoriosRepository, LaboratoriosRepository>();

        builder.Services.AddScoped<ILaboratoriosService, LaboratoriosService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}