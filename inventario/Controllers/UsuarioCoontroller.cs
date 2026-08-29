using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;

using Microsoft.AspNetCore.Mvc;


namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioCoontroller:ControllerBase
{
    private readonly AppDbInventario _context;

    public UsuarioCoontroller(AppDbInventario context)
    {
        _context = context; 
    }
    
    

}