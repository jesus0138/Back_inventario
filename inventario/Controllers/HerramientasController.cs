using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.OpenApi;
using inventario.Dtos;
using Microsoft.VisualBasic;

namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HerramientasController:ControllerBase
{
    private readonly AppDbInventario _context;

    public HerramientasController(AppDbInventario context)
    {
        _context = context; 
    }

    [HttpGet]
    public async Task<IActionResult> GetHerramientas()
    {
        var herramientas=await _context.Herramientas.ToListAsync();
        return Ok(herramientas);
    }

    [HttpPost]
    public async Task<IActionResult> PostHerramientas(Herramienta herramienta)
    {
        bool existe = await _context.Herramientas
            .AnyAsync(a => a.Id == herramienta.Id);
        if (existe==true)
        {
            return BadRequest("asignacion existente");
        }
        _context.Herramientas.Add(herramienta);
        await _context.SaveChangesAsync();
        return Ok(herramienta);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHerramientas(int id)
    {
        var herramienta = await _context.Herramientas.FindAsync(id);
        if (herramienta==null)
        {
            return NotFound();
        }

        _context.Herramientas.Remove(herramienta);
        await _context.SaveChangesAsync();
        return Ok(herramienta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutHerramientas(int id, Herramienta herramienta)
    {
        var existente = await _context.Herramientas.FindAsync(id);
        if (existente==null)
        {
            return NotFound();
        }
        existente.Color=herramienta.Color;
        existente.FechaAdquisicion = herramienta.FechaAdquisicion;
        existente.Marca = herramienta.Marca;
        existente.Modelo=herramienta.Modelo;
        existente.Nombre = herramienta.Nombre;
        existente.Stock=herramienta.Stock;
        existente.Tipo = herramienta.Tipo;
        existente.Valor=herramienta.Valor;
        await _context.SaveChangesAsync();
        return Ok(existente);
        
    }



}