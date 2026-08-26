using System.ComponentModel.DataAnnotations;
namespace inventario.Models;

public class Rol
{
    public int Id { get; set; }
    [MaxLength(100)]
    public required string Nombre { get; set; }
    
}