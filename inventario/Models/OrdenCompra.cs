namespace inventario.Models;

public class OrdenCompra
{
    public int Id { get; set; }
    public string NumeroOrden { get; set; } = "";
    public DateTime Fecha { get; set; }
    public required string Destinatario { get; set; }
    public required string Email { get; set; }
    public required string Conductor  { get; set; }
    public required int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; } = null!;
    public  string? Observaciones { get; set; }
    public required string CantidadLetras { get; set; }
    public required string Elaborado { get; set; }
    public required decimal TotalOrden { get; set; }
    public virtual ICollection<CompraItem> Items { get; set; } = new List<CompraItem>();
    

}