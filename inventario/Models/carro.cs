using  System.ComponentModel.DataAnnotations;
namespace inventario.Models;

public class Carro
{
    public int Id { get; set; }
    [MaxLength(10)]
    public required string Placa { get; set; }
    [MaxLength(50)]
    public required string Vin { get; set; } 
    [MaxLength(100)]
    public required string Marca { get; set; } 
    [MaxLength(100)]
    public required string Modelo { get; set; }
    
    public required int Anio { get; set; }
    [MaxLength(100)]
    public required string Tipo { get; set; }
    [MaxLength(100)]
    public required string Color { get; set; }
    [MaxLength(15)]
    public required string Estado { get; set; } 
    public DateTime FechaAdquisicion { get; set; } = DateTime.UtcNow;

    public decimal Valor  { get; set; } 
}