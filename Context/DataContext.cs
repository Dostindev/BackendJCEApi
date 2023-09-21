global using Microsoft.EntityFrameworkCore;
using BackendApi.Models;

namespace BackendApi.Context;

public class DataContext : DbContext
{
    private const string ConnectionString = "server=DESKTOP-0AG0BLJ\\SQLEXPRESS;Database=UniversidadJCE; Integrated Security=true; TrustServerCertificate=true;";
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profesor>()
         .HasMany(p => p.Cursos) // Un profesor tiene muchos cursos
         .WithOne(c => c.Profesor) // Un Curso pertenece a un profesor
         .HasForeignKey(c => c.ProfesorId);

        modelBuilder.Entity<Curso>()
        .HasMany(c => c.Estudiantes) // Un Curso tiene muchos estudiantes
        .WithOne(e => e.Curso) // Un estudiante pertenece a un Curso
        .HasForeignKey(e => e.CursoId);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConnectionString);
    }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<Estudiante> Estudiantes { get; set; }
}
