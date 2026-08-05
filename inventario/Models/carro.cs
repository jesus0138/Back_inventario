

namespace inventario.Models;

public class Carro
{
    public int Id { get; set; }
    public required string Placa { get; set; }
    public required string Vin { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public required int Anio { get; set; }
    public required string Tipo { get; set; }
    public required string Color { get; set; }
    public required string Estado { get; set; }
    public DateTime FechaAdquisicion { get; set; } = DateTime.UtcNow;
public decimal Valor  { get; set; }
}