using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.OpenApi;


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
    public async Task<IActionResult> PostAsignacionCarro(AsignacionCarro asignacionCarro)
    {
        bool existe = await _context.AsignacionCarros
            .AnyAsync(a => a.CarroId == asignacionCarro.CarroId && a.FechaDevolucion == null);
        if (existe==true)
        {
            return BadRequest("asignacion existente");
        }

        _context.AsignacionCarros.Add(asignacionCarro);
        await _context.SaveChangesAsync();
        return Ok(asignacionCarro);
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
    public async Task<IActionResult> PutAsignacionCarro(int id, AsignacionCarro asignacionCarro)
    {
        var existente = await _context.AsignacionCarros.FindAsync(id);
        if (existente==null)
        {
            return NotFound();
        }

        existente.PersonaId = asignacionCarro.PersonaId;
        existente.AsignadoPorUsuarioId = asignacionCarro.AsignadoPorUsuarioId;
        existente.CarroId = asignacionCarro.CarroId;
        existente.FechaAsignacion = asignacionCarro.FechaAsignacion;
        existente.FechaDevolucion = asignacionCarro.FechaDevolucion;
        await _context.SaveChangesAsync();
        return Ok();
    }
}