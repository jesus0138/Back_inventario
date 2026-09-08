using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using inventario.Dtos;

namespace inventario.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CuadrillaController:ControllerBase
{
    private readonly AppDbInventario _context;

    public CuadrillaController(AppDbInventario context)
    {
        _context=context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCuadrilla()
    {
        var cuadrilla = await _context.Cuadrillas.ToListAsync();
            return Ok(cuadrilla);
    }

    [HttpPost]
    public async Task<IActionResult> PostCuadrilla(CuadrillasCreateDto  cuadrilla){
        bool existe=await _context.Cuadrillas
            .AnyAsync(e=>e.Numero==cuadrilla.Numero);
        if (existe==true)
        {
            return BadRequest("Cuadrilla existente");
        }

        var cuadrillas = new Cuadrilla
        {
            Numero = cuadrilla.Numero,
            Sector = cuadrilla.Sector,
            ProcesoId = cuadrilla.ProcesoId

        };
        _context.Cuadrillas.Add(cuadrillas);
        await _context.SaveChangesAsync();
        return Ok(cuadrillas);
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
        public async Task<IActionResult> PutCuadrilla(int id, Cuadrilla cuadrilla)
        {
            var existente = await _context.Cuadrillas.FindAsync(id);
            if (existente==null)
            {
                return NotFound();
            }
            existente.Numero=cuadrilla.Numero;
            existente.ProcesoId=cuadrilla.ProcesoId;
            existente.Sector = cuadrilla.Sector;
            await _context.SaveChangesAsync();
            return Ok(cuadrilla);
        }
}