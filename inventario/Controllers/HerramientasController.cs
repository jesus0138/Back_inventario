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
        bool existe = await _context.AsignacionCarros
            .AnyAsync(a => a.Id == herramienta.Id);
        if (existe==true)
        {
            return BadRequest("asignacion existente");
        }
        _context.Herramientas.Add(herramienta);
        await _context.SaveChangesAsync();
        return Ok(herramienta);
    }
    
    
    

}