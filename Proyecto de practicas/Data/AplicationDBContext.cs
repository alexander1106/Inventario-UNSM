using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto_de_practicas.Models;



namespace Proyecto_de_practicas.Data
{
    public class AplicationDBContext: DbContext
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options) { }
        public DbSet<Laboratorios> Laboratorios { get; set; }
        public DbSet<Aulas> Aulas { get; set; }
        public DbSet<Equipos> Equipos { get;  set; }
        public DbSet<Pisos> Pisos { get; set; }
        public DbSet<Categorias> Categorias {get ; set;}
        public DbSet<Facultades> Facultades { get; set; }
}
}
