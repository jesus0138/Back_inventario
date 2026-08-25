using  System.ComponentModel.DataAnnotations;
namespace inventario.Models;

public class Usuario
{
    public int Id { get; set; }
    [MaxLength(100)]
    public required string NombreUsuario { get; set; }  
    [MaxLength(100)]
    public required string Password { get; set; } // la contrasena va hasheada
    public int RolId { get; set; }
    public virtual Rol Rol { get; set;}=null!;

    public int? ProcesoId { get; set; }
    public virtual Proceso? Proceso { get; set; } // en si en este caso queda como no obligatoria por el signo se interrogacio.
}