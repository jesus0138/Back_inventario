namespace inventario.Models;

public class Persona
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public required string Identidad { get; set; }
    public required string Telefono { get; set; }
    public required string Cargo { get; set; }
    public required string CuadrillaId { get; set; }
    
}