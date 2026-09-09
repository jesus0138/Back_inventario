using inventario.Models;
 using Microsoft.EntityFrameworkCore;
 using inventario.Data;
 using inventario.Dtos;
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
     public async Task<IActionResult> PostPersona(PersonaCreateDto dto)
     { bool existe=await _context.Personas
             .AnyAsync(e=>e.Identidad==dto.Identidad);
         if (existe==true)
         {
             return BadRequest("Persona existente");
         }
 
         var personas = new Persona
         {
             Nombre = dto.Nombre,
             Identidad = dto.Identidad,
             Telefono = dto.Telefono,
             Cargo = dto.Cargo,
             CuadrillaId = dto.CuadrillaId
 
 
         };
         _context.Personas.Add(personas);
         await _context.SaveChangesAsync();
         return Ok(personas);
     }
 
     [HttpDelete("{id}")]
 
     public async Task<IActionResult> DeletePersona(int id)
     {
         var persona = await _context.Personas.FindAsync(id);
         if (persona==null)
         {
             return NotFound();
         }
 
         _context.Personas.Remove(persona);
         await _context.SaveChangesAsync();
         return Ok(persona);
 
     }
 
     [HttpPut("{id}")]
     public async Task<IActionResult> PutPersona(int id, PersonaCreateDto dto)
     {
         var personaExistente = await _context.Personas.FindAsync(id);
         if (personaExistente==null)
         {
             return NotFound();
         }
 
        
         
         personaExistente.Nombre = dto.Nombre;
         personaExistente.Identidad=dto.Identidad;
         personaExistente.Telefono=dto.Telefono;
         personaExistente.Cargo = dto.Cargo;
         personaExistente.CuadrillaId=dto.CuadrillaId;
         await _context.SaveChangesAsync();
         return Ok(personaExistente);
     }
 }