using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using Microsoft.AspNetCore.Mvc;
using inventario.Dtos;

namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsignacionCarroController : ControllerBase
{
    private readonly AppDbInventario _context;

    public AsignacionCarroController(AppDbInventario context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsignacionesCarro()
    {
        var asignacionCarros = await _context.AsignacionCarros
            .Include(a => a.Carro)
            .Include(a => a.Persona)
            .Include(a => a.AsignadoPorUsuario)
            .ToListAsync();
        return Ok(asignacionCarros);
    }

    [HttpGet("activas")]
    public async Task<IActionResult> GetAsignacionesCarroActivas()
    {
        var activas = await _context.AsignacionCarros
            .Include(a => a.Carro)
            .Include(a => a.Persona)
            .Include(a => a.AsignadoPorUsuario)
            .Where(a => a.FechaDevolucion == null)
            .ToListAsync();
        return Ok(activas);
    }

    [HttpGet("persona/{personaId}")]
    public async Task<IActionResult> GetAsignacionesCarroPorPersona(int personaId)
    {
        var asignaciones = await _context.AsignacionCarros
            .Include(a => a.Carro)
            .Include(a => a.Persona)
            .Include(a => a.AsignadoPorUsuario)
            .Where(a => a.PersonaId == personaId && a.FechaDevolucion == null)
            .ToListAsync();
        return Ok(asignaciones);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsignacionCarro(AsignacionCarroCreateDto dto)
    {
        var carro = await _context.Carros.FindAsync(dto.CarroId);
        if (carro == null)
        {
            return NotFound("Carro no encontrado");
        }

        bool yaAsignado = await _context.AsignacionCarros
            .AnyAsync(a => a.CarroId == dto.CarroId && a.FechaDevolucion == null);
        if (yaAsignado)
        {
            return BadRequest("Este carro ya está asignado actualmente");
        }

        var ultimaAsignacion = await _context.AsignacionCarros
            .Where(a => a.CarroId == dto.CarroId && a.FechaDevolucion != null)
            .OrderByDescending(a => a.FechaDevolucion)
            .FirstOrDefaultAsync();

        if (ultimaAsignacion != null &&
            (ultimaAsignacion.EstadoDevolucion == "Dañado" || ultimaAsignacion.EstadoDevolucion == "En reparación"))
        {
            return BadRequest($"Este carro no se puede asignar. Estado actual: {ultimaAsignacion.EstadoDevolucion}");
        }

        var asignacion = new AsignacionCarro
        {
            PersonaId = dto.PersonaId,
            CarroId = dto.CarroId,
            AsignadoPorUsuarioId = dto.AsignadoPorUsuarioId,
            FechaAsignacion = DateTime.UtcNow
        };

        _context.AsignacionCarros.Add(asignacion);
        await _context.SaveChangesAsync();
        return Ok(asignacion);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsignacionCarro(int id)
    {
        var asig = await _context.AsignacionCarros.FindAsync(id);
        if (asig == null)
        {
            return NotFound();
        }

        _context.AsignacionCarros.Remove(asig);
        await _context.SaveChangesAsync();
        return Ok(asig);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsignacionCarro(int id, AsignacionCarroCreateDto dto)
    {
        var existente = await _context.AsignacionCarros.FindAsync(id);
        if (existente == null)
        {
            return NotFound();
        }

        existente.PersonaId = dto.PersonaId;
        existente.AsignadoPorUsuarioId = dto.AsignadoPorUsuarioId;
        existente.CarroId = dto.CarroId;
        await _context.SaveChangesAsync();
        return Ok(existente);
    }

    [HttpPatch("{id}/devolver")]
    public async Task<IActionResult> DevolverCarro(int id, DevolucionCarro dto)
    {
        var existente = await _context.AsignacionCarros.FindAsync(id);
        if (existente == null)
        {
            return NotFound();
        }

        if (existente.FechaDevolucion != null)
        {
            return BadRequest("Esta asignación ya fue devuelta anteriormente");
        }

        existente.FechaDevolucion = DateTime.UtcNow;
        existente.EstadoDevolucion = dto.EstadoDevolucion;
        await _context.SaveChangesAsync();
        return Ok(existente);
    }

    [HttpPatch("carro/{carroId}/reparar")]
    public async Task<IActionResult> MarcarCarroReparado(int carroId)
    {
        var ultimaAsignacion = await _context.AsignacionCarros
            .Where(a => a.CarroId == carroId && a.FechaDevolucion != null)
            .OrderByDescending(a => a.FechaDevolucion)
            .FirstOrDefaultAsync();

        if (ultimaAsignacion == null)
        {
            return NotFound("No hay historial de devolución para este carro");
        }

        if (ultimaAsignacion.EstadoDevolucion != "Dañado" && ultimaAsignacion.EstadoDevolucion != "En reparación")
        {
            return BadRequest("Este carro no está marcado como dañado o en reparación");
        }

        ultimaAsignacion.EstadoDevolucion = "Bueno";
        await _context.SaveChangesAsync();
        return Ok(ultimaAsignacion);
    }
}