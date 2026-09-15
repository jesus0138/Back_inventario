namespace inventario.Dtos;

public class HerramientaCreateDto
{
    public required string Nombre { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public required string Tipo { get; set; }
    public required string Color { get; set; }
    public required int Stock { get; set; }
    public required decimal Valor { get; set; }
    // FechaAdquisicion se genera en el servidor, no la manda el cliente
}