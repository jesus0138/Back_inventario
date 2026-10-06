using System.ComponentModel.DataAnnotations;

namespace inventario.Models;

public class OrdenCompra
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string NumeroOrden { get; set; } = "";
    public DateTime Fecha { get; set; }
    [MaxLength(200)]
    public required string Destinatario { get; set; }
    [MaxLength(200)]
    public required string Email { get; set; }
    [MaxLength(200)]
    public required string Conductor  { get; set; }
    public required int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; } = null!;
    [MaxLength(200)]
    public  string? Observaciones { get; set; }
    [MaxLength(200)]
    public required string CantidadLetras { get; set; }
    [MaxLength(200)]
    public required string Elaborado { get; set; }
    public required decimal TotalOrden { get; set; }
    public virtual ICollection<CompraItem> Items { get; set; } = new List<CompraItem>();
    

}