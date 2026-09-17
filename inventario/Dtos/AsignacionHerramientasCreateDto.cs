namespace inventario.Dtos;

public class AsignacionHerramientaCreateDto
{
    public required int HerramientaId { get; set; }
    public required int PersonaId { get; set; }
    public required int CuadrillaId { get; set; }
    public required int Cantidad { get; set; }
   
}
public class DevolucionDto
{
    public required string EstadoDevolucion { get; set; }
}