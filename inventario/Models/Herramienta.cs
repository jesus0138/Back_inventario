using System.ComponentModel.DataAnnotations;
namespace inventario.Models;

public class Herramienta
{
    public int Id { get; set; }
    [MaxLength(100)]
    public required string Nombre { get; set; }
    [MaxLength(100)]
    public required string Marca { get; set; }
    [MaxLength(100)]
    public required string Modelo { get; set; }
    public required string Tipo { get; set; }
    [MaxLength(100)]
    public required string Color { get; set; }
    public required int Stock { get; set; }

    public DateTime FechaAdquisicion { get; set; } = DateTime.UtcNow;

    public decimal Valor { get; set; }
}