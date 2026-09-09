using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using BCrypt.Net;
using inventario.Dtos;
using Microsoft.AspNetCore.Mvc;
using inventario.Services;

namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{
    private readonly AppDbInventario _context;
    private readonly ITokenService _tokenService;
    public AuthController(AppDbInventario context, ITokenService tokenService)
    {
        _context = context; 
        _tokenService = tokenService;
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
        if (existe == true)
        {
            var token = _tokenService.GenerarToken(usuario);
            return Ok(new { nombreUsuario = usuario.NombreUsuario, token = token });
        }
        else
        {
            return BadRequest("Contraseña o usuario incorrecto");
        }

      
    }
}
