using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;


namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController:ControllerBase
{
    private readonly AppDbInventario _context;

    public UsuarioController(AppDbInventario context)
    {
        _context = context; 
    }

    [HttpGet]

    public async Task<IActionResult> GetUsuarios()
    {
        var usuario = await _context.Usuarios.ToListAsync();
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> PostUsuario(Usuario usuario)
    {
        bool existe=await _context.Usuarios
            .AnyAsync(n=>n.NombreUsuario==usuario.NombreUsuario);
        if (existe==true)
        {
            return BadRequest("El nombre de usuario ya existe");
        }
        usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return Ok();
        
    }
    //necesito terminar de hacer el metodo delete y put
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario==null)
        {
            return NotFound();
        }
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        
        return Ok(usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
        var existente = await _context.Usuarios.FindAsync(id);
        if (existente==null)
        {
            return NotFound(id);
        }

        existente.NombreUsuario = usuario.NombreUsuario;
        existente.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
        existente.RolId = usuario.RolId; 
        existente.ProcesoId = usuario.ProcesoId;

        await _context.SaveChangesAsync();
        return Ok();
    }
}