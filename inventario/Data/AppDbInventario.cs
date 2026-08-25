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
}