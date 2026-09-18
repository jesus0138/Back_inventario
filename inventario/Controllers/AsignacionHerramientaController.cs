using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using Microsoft.AspNetCore.Mvc;
using inventario.Dtos;
namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsignacionHerramientaController : ControllerBase
{
    private readonly AppDbInventario _context;

    public AsignacionHerramientaController(AppDbInventario context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsignaciones()
    {
        var asignaciones = await _context.AsignacionHerramientas
            .Include(a => a.Herramienta)
            .Include(a => a.Persona)
            .Include(a => a.Cuadrilla)
            .ToListAsync();
        return Ok(asignaciones);
    }

    [HttpGet("activas")]
    public async Task<IActionResult> GetAsignacionesActivas()
    {
        var activas = await _context.AsignacionHerramientas
            .Include(a => a.Herramienta)
            .Include(a => a.Persona)
            .Include(a => a.Cuadrilla)
            .Where(a => a.FechaDevolucion == null)
            .ToListAsync();
        return Ok(activas);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsignacion(AsignacionHerramientaCreateDto dto)
    {
        var herramienta = await _context.Herramientas.FindAsync(dto.HerramientaId);
        if (herramienta == null)
        {
            return NotFound("Herramienta no encontrada");
        }

        var personaExiste = await _context.Personas.AnyAsync(p => p.Id == dto.PersonaId);
        if (!personaExiste)
        {
            return NotFound("Persona no encontrada");
        }

        var cuadrillaExiste = await _context.Cuadrillas.AnyAsync(c => c.Id == dto.CuadrillaId);
        if (!cuadrillaExiste)
        {
            return NotFound("Cuadrilla no encontrada");
        }

        int cantidadPrestadaActual = await _context.AsignacionHerramientas
            .Where(a => a.HerramientaId == dto.HerramientaId && a.FechaDevolucion == null)
            .SumAsync(a => a.Cantidad);
        int cantidadDañada = await _context.AsignacionHerramientas
            .Where(a => a.HerramientaId == dto.HerramientaId && a.EstadoDevolucion == "Dañada")
            .SumAsync(a => a.Cantidad);

        int disponible = herramienta.Stock - cantidadPrestadaActual - cantidadDañada;
        

        if (dto.Cantidad > disponible)
        {
            return BadRequest($"Stock insuficiente. Disponible: {disponible}, solicitado: {dto.Cantidad}");
        }

        var asignacion = new AsignacionHerramienta
        {
            HerramientaId = dto.HerramientaId,
            PersonaId = dto.PersonaId,
            CuadrillaId = dto.CuadrillaId,
            Cantidad = dto.Cantidad,
            FechaAsignacion = DateTime.UtcNow,
            FechaDevolucion = null
        };

        _context.AsignacionHerramientas.Add(asignacion);
        await _context.SaveChangesAsync();

        return Ok(asignacion);
    }

    [HttpPut("{id}/devolver")]
    public async Task<IActionResult> DevolverHerramienta(int id, DevolucionDto dto)
    {
        var asignacion = await _context.AsignacionHerramientas.FindAsync(id);
        if (asignacion == null)
        {
            return NotFound();
        }

        if (asignacion.FechaDevolucion != null)
        {
            return BadRequest("Esta asignación ya fue devuelta anteriormente");
        }

        asignacion.FechaDevolucion = DateTime.UtcNow;
        asignacion.EstadoDevolucion = dto.EstadoDevolucion;
        await _context.SaveChangesAsync();

        return Ok(asignacion);
    }
    [HttpGet("danadas")]
    public async Task<IActionResult> GetAsignacionesDanadas()
    {
        var danadas = await _context.AsignacionHerramientas
            .Include(a => a.Herramienta)
            .Include(a => a.Persona)
            .Include(a => a.Cuadrilla)
            .Where(a => a.EstadoDevolucion == "Dañada")
            .ToListAsync();
        return Ok(danadas);
    }
    [HttpGet("persona/{personaId}")]
    public async Task<IActionResult> GetAsignacionesPorPersona(int personaId)
    {
        var asignaciones = await _context.AsignacionHerramientas
            .Include(a => a.Herramienta)
            .Include(a => a.Persona)
            .Include(a => a.Cuadrilla)
            .Where(a => a.PersonaId == personaId && a.FechaDevolucion == null)
            .ToListAsync();
        return Ok(asignaciones);
    }

    [HttpGet("cuadrilla/{cuadrillaId}")]
    public async Task<IActionResult> GetAsignacionesPorCuadrilla(int cuadrillaId)
    {
        var asignaciones = await _context.AsignacionHerramientas
            .Include(a => a.Herramienta)
            .Include(a => a.Persona)
            .Include(a => a.Cuadrilla)
            .Where(a => a.CuadrillaId == cuadrillaId && a.FechaDevolucion == null)
            .ToListAsync();
        return Ok(asignaciones);
    }
}