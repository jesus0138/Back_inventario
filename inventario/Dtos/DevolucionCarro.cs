namespace inventario.Dtos;
public class DevolucionCarro
{
    public required string EstadoDevolucion { get; set; }
}

public class RepararCarroDto
{
    // No necesita campos, es solo una acción — pero lo dejamos como clase
    // por si en el futuro quieres agregar notas de reparación, costo, etc.
}