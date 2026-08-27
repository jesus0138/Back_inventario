using inventario.Data;
using inventario.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventario.Controllers;
[ApiController]
[Route("api/[controller]")] 
public class ProcesoController: ControllerBase
{
    
    
    
        private readonly AppDbInventario _context;

        public ProcesoController(AppDbInventario context)
        {
                _context=context; }
        [HttpGet] 
        public async Task<IActionResult> GetProceso ()
        {
                var procesos = await _context.Procesos.ToListAsync();
                return Ok(procesos);
        }

        [HttpPost]
        public async Task<IActionResult> PostProceso(Proceso proceso)
        {
                _context.Procesos.Add(proceso);
                await _context.SaveChangesAsync();
                return Ok(proceso);
        }
}