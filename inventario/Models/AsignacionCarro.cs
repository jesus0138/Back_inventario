using System.ComponentModel.DataAnnotations.Schema;
namespace inventario.Models;



public class AsignacionCarro
{
   public int Id { get; set; }
   public required int PersonaId { get; set; }
   public virtual Persona Persona { get; set; } = null!;
    public required int CarroId { get; set; }
    public virtual Carro Carro { get; set; } = null!;
    public required int AsignadoPorUsuarioId { get; set; }
    public required DateTime FechaAsignacion { get; set; }
    public  DateTime? FechaDevolucion { get; set; }
    [ForeignKey("AsignadoPorUsuarioId")]
    public virtual Usuario AsignadoPorUsuario { get; set; } = null!;
}