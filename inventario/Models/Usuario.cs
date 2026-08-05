namespace inventario.Models;

public class Usuario
{
    public int Id { get; set; }
    public required string NombreUsuario { get; set; }  
    public required string Password { get; set; } // la contrasena va hasheada
    public int RolId { get; set; }
    public  Rol Rol { get; set;}=null!;

    public int? ProcesoId { get; set; }
    public virtual Proceso Proceso { get; set; } = null!;
}