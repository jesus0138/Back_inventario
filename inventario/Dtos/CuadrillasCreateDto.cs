using Microsoft.AspNetCore.Components.Sections;

namespace inventario.Dtos;

public class CuadrillasCreateDto
{
    public required int Numero{get; set;}
    public required string Sector { get; set; }
    public required int ProcesoId { get; set; }
}