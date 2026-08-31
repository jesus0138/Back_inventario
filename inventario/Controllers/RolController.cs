
using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;

using Microsoft.AspNetCore.Mvc;


namespace inventario.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RolController:ControllerBase
{
    
    private readonly AppDbInventario _context;
    public RolController(AppDbInventario context)
    {
        _context = context;     
    }

    [HttpGet]
    public async Task<ActionResult> GetRol()
    {
        var roles = await _context.Roles.ToListAsync();
        return Ok(roles);
    }


    [HttpPost]
    public async Task<IActionResult> PostRol(Rol rol)
    { 
        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();
        return Ok(rol);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRol(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol==null)
        {
            return NotFound();
        }

        _context.Roles.Remove(rol);
        await _context.SaveChangesAsync();
        return Ok(rol);
        

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRol(int id, Rol rol)
    {
        var existente = await _context.Roles.FindAsync(id);
        if (existente==null)
        {
            return NotFound();
        }
        existente.Nombre=rol.Nombre;
        await _context.SaveChangesAsync();
        return Ok(rol);

    }
}