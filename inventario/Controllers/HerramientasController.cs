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
        var herramientas = await _context.Herramientas.ToListAsync();

        var prestadoPorHerramienta = await _context.AsignacionHerramientas
            .Where(a => a.FechaDevolucion == null)
            .GroupBy(a => a.HerramientaId)
            .Select(g => new { HerramientaId = g.Key, Prestado = g.Sum(a => a.Cantidad) })
            .ToDictionaryAsync(x => x.HerramientaId, x => x.Prestado);

        var resultado = herramientas.Select(h => new
        {
            h.Id,
            h.Nombre,
            h.Marca,
            h.Modelo,
            h.Tipo,
            h.Color,
            h.Stock,
            Disponible = h.Stock - (prestadoPorHerramienta.ContainsKey(h.Id) ? prestadoPorHerramienta[h.Id] : 0),
            h.FechaAdquisicion,
            h.Valor
        });

        return Ok(resultado);
    }

    [HttpPost]
    public async Task<IActionResult> PostHerramientas(HerramientaCreateDto dto)
    {
        var herramienta = new Herramienta
        {
            Nombre = dto.Nombre,
            Marca = dto.Marca,
            Modelo = dto.Modelo,
            Tipo = dto.Tipo,
            Color = dto.Color,
            Stock = dto.Stock,
            Valor = dto.Valor,
            FechaAdquisicion = DateTime.UtcNow
        };

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