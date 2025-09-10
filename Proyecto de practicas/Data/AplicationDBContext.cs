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

    }
}
