namespace inventario.Dtos;

public class PersonaCreateDto
{
    public required string Nombre { get; set; }
    public required string Identidad { get; set; }
    public required string Telefono { get; set; }
    public required string Cargo { get; set; }
    public required int CuadrillaId { get; set; }
}