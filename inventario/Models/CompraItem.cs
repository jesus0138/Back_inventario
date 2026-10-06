using System. ComponentModel.DataAnnotations;
namespace inventario.Models;

public class CompraItem
{
    public int Id { get; set; }
    [MaxLength(200)]
    public required string Itemname {get ; set; }
    public required decimal Cantidad { get; set; }
    [MaxLength(200)]
    public required string Descripcion { get; set; }
   public required decimal Total { get; set; }
   public required decimal PrecioUnitario { get; set; }
   public required int OrdenCompraId { get; set; }
   public virtual OrdenCompra OrdenCompra { get; set; } = null!;
}