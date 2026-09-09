using System.ComponentModel.DataAnnotations;
namespace inventario.Models;

public class AsignacionHerramienta
{
    public int Id { get; set; }

    public int HerramientaId { get; set; }
    public virtual Herramienta Herramienta { get; set; } = null!;

    public int PersonaId { get; set; }
    public virtual Persona Persona { get; set; } = null!;

    public int CuadrillaId { get; set; }
    public virtual Cuadrilla Cuadrilla { get; set; } = null!;

    public required int Cantidad { get; set; }

    public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaDevolucion { get; set; }
}