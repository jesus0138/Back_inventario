using inventario.Models;
using Microsoft.AspNetCore.Builder;
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
        var PersonaExistente = await _context.Personas.FindAsync(id);
        if (PersonaExistente==null)
        {
            return NotFound();
        }

       
        
        PersonaExistente.Nombre = persona.Nombre;
        PersonaExistente.Identidad=persona.Identidad;
        PersonaExistente.Telefono=persona.Telefono;
        PersonaExistente.Cargo = persona.Cargo;
        PersonaExistente.CuadrillaId=persona.CuadrillaId;
        await _context.SaveChangesAsync();
        return Ok(PersonaExistente);
    }
}