namespace inventario.Dtos;

public class UsuarioCreateDto
{
    public required string NombreUsuario { get; set; }
    public required string Password { get; set; }
    public int RolId { get; set; }
    public int? ProcesoId { get; set; }
}