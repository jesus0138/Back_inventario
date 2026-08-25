using System.ComponentModel.DataAnnotations;
namespace inventario.Models;

public class Persona
{
    public int Id { get; set; }
    [MaxLength(100)]
    public required string Nombre { get; set; }
    [MaxLength(20)]
    public required string Identidad { get; set; }
    [MaxLength(20)]
    public required string Telefono { get; set; }
    [MaxLength(50)]
    public required string Cargo { get; set; }
   
    public required int  CuadrillaId { get; set; }
    public virtual Cuadrilla Cuadrilla { get; set; } = null!; //este esta en estado obligatiro diferente de null

}