using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using BCrypt.Net;
using inventario.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace inventario.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly AppDbInventario _context;
    
    public AuthController(AppDbInventario context)
    {
        _context = context; 
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NombreUsuario == loginDto.NombreUsuario);
        if (usuario == null)
        {
            return BadRequest("Usuario no encontrado o Contreseña incorrecta");
        }
        bool existe=BCrypt.Net.BCrypt.Verify(loginDto.Password,usuario.Password);

        return Ok();
    }
}
