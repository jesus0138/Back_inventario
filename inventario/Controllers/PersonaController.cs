using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using Microsoft.AspNetCore.Mvc;

namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PersonaController: ControllerBase
{
    private readonly AppDbInventario _context;
    public PersonaController(AppDbInventario context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPersona()
    {
        var personas = await _context.Personas.ToListAsync();
        return Ok(personas);
    }

    [HttpPost]
    public async Task<IActionResult> PostPersona(Persona persona)
    {
        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();
        return Ok(persona);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteCuadrilla(int id)
    {
        var cuadrilla = await _context.Cuadrillas.FindAsync(id);
        if (cuadrilla==null)
        {
            return NotFound();
        }

        _context.Cuadrillas.Remove(cuadrilla);
        await _context.SaveChangesAsync();
        return Ok(cuadrilla);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPersona(int id, Persona persona)
    {
        var personaExistente = await _context.Personas.FindAsync(id);
        if (personaExistente==null)
        {
            return NotFound();
        }

       
        
        personaExistente.Nombre = persona.Nombre;
        personaExistente.Identidad=persona.Identidad;
        personaExistente.Telefono=persona.Telefono;
        personaExistente.Cargo = persona.Cargo;
        personaExistente.CuadrillaId=persona.CuadrillaId;
        await _context.SaveChangesAsync();
        return Ok(personaExistente);
    }
}