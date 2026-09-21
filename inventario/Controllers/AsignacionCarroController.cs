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
        bool existe = await _context.AsignacionCarros
            .AnyAsync(a => a.CarroId == dto.CarroId && a.FechaDevolucion == null);
        if (existe)
        {
            return BadRequest("Este carro ya está asignado actualmente");
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
    public async Task<IActionResult> PatchAsignacionCarro(int id)
    {
        var existente = await _context.AsignacionCarros.FindAsync(id);
        if (existente == null)
        {
            return NotFound();
        }
        existente.FechaDevolucion = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return Ok(existente);
    }
}