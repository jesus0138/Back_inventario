

namespace inventario.Models;

public class Cuadrilla
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string Sector { get; set; } =null!;
    public required string  ProcesoId { get; set; }
    public virtual Proceso Proceso { get; set; } = null!;
}