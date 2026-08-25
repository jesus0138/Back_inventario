using System. ComponentModel.DataAnnotations;

namespace inventario.Models;

public class Cuadrilla
{
    public int Id { get; set; }
    public int Numero { get; set; }
    [MaxLength(100)]
    public required string Sector { get; set; }
    
    public required int  ProcesoId { get; set; }
    public virtual Proceso Proceso { get; set; } = null!;
}