using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.OpenApi;
using inventario.Dtos;
using Microsoft.VisualBasic;

namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsignacionCarroController:ControllerBase
{
    private readonly AppDbInventario _context;

    public AsignacionCarroController(AppDbInventario context)
    {
        _context = context; 
    }

    [HttpGet]
    public async Task<IActionResult> GetCuadrillas()
    {
       var asignacionCarros = await _context.AsignacionCarros.ToListAsync();
        return Ok(asignacionCarros);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsignacionCarro(AsignacionCarroCreateDto dto)
    {
        bool existe = await _context.AsignacionCarros
            .AnyAsync(a => a.CarroId == dto.CarroId && a.FechaDevolucion == null);
        if (existe==true)
        {
            return BadRequest("asignacion existente");
        }

        var asignacion = new AsignacionCarro
        {
            PersonaId = dto.PersonaId,
            CarroId = dto.CarroId,
            AsignadoPorUsuarioId = dto.AsignadoPorUsuarioId,
            FechaAsignacion =DateTime.UtcNow


        };
        

        _context.AsignacionCarros.Add(asignacion);
        await _context.SaveChangesAsync();
        return Ok(asignacion);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsignacionCarro(int id)
    {
        var asig = await _context.AsignacionCarros.FindAsync(id);
        if (asig==null)
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
        if (existente==null)
        {
            return NotFound();
        }

        existente.PersonaId = dto.PersonaId;
        existente.AsignadoPorUsuarioId = dto.AsignadoPorUsuarioId;
        existente.CarroId = dto.CarroId;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch("{id}/devolver")]
    public async Task<IActionResult> PatchAsignacionCarro(int id)
    {
        var existente = await _context.AsignacionCarros.FindAsync(id);
        if (existente==null)
        {
            return NotFound();
        }
        existente.FechaDevolucion= DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return Ok();
        
    }
}