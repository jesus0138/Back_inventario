using Microsoft.EntityFrameworkCore;
using inventario.Models;
namespace inventario.Data;

public class AppDbInventario : DbContext
{
    public AppDbInventario(DbContextOptions<AppDbInventario> options) : base(options)
    {

    }

    public DbSet<Carro> Carros { get; set; }
    public DbSet<AsignacionCarro> AsignacionCarros { get; set; }
    public DbSet<Cuadrilla> Cuadrillas { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Proceso> Procesos { get; set; }
    public DbSet<AsignacionHerramienta> AsignacionHerramientas { get; set; }
    public DbSet<Herramienta> Herramientas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AsignacionHerramienta>()
            .HasOne(a => a.Herramienta)
            .WithMany()
            .HasForeignKey(a => a.HerramientaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AsignacionHerramienta>()
            .HasOne(a => a.Persona)
            .WithMany()
            .HasForeignKey(a => a.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AsignacionHerramienta>()
            .HasOne(a => a.Cuadrilla)
            .WithMany()
            .HasForeignKey(a => a.CuadrillaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AsignacionCarro>()
            .HasOne(a => a.Persona)
            .WithMany()
            .HasForeignKey(a => a.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AsignacionCarro>()
            .HasOne(a => a.Carro)
            .WithMany()
            .HasForeignKey(a => a.CarroId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AsignacionCarro>()
            .HasOne(a => a.AsignadoPorUsuario)
            .WithMany()
            .HasForeignKey(a => a.AsignadoPorUsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}