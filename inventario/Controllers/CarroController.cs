using inventario.Models;
using Microsoft.EntityFrameworkCore;
using inventario.Data;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;


namespace inventario.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarroController:ControllerBase
{
    private readonly AppDbInventario _context;

    public CarroController(AppDbInventario context)
    {
        _context = context; 
    }

    [HttpGet]
    public async Task<IActionResult> GetCarro()
    {
        var carros = await _context.Carros.ToListAsync();
        return Ok(carros);
    }

    [HttpPost] 
    public async Task<IActionResult> PostCarro(Carro carro)
    {
        bool existe=await _context.Carros
            .AnyAsync(n=>n.Placa==carro.Placa);
        if (existe==true)
        {
            return BadRequest("Placa ya registrada");
        }

        _context.Carros.Add(carro);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCarro(int id)
    {
        var carro = await _context.Carros.FindAsync(id);
        if (carro==null)
        {
            return NotFound();
        }

        _context.Carros.Remove(carro);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCarro(int id, Carro carro)
    {
        var existente = await _context.Carros.FindAsync(id);
        if (existente==null)
        {
            return NotFound(id);
        }
        existente.Placa=carro.Placa;
        existente.Anio=carro.Anio;
        existente.Color=carro.Color;
        existente.Estado=carro.Estado;
        existente.FechaAdquisicion = carro.FechaAdquisicion;
        existente.Marca=carro.Marca;
        existente.Modelo=carro.Modelo;
        existente.Tipo=carro.Tipo;
        existente.Valor=carro.Valor;
        existente.Vin = carro.Vin;
        await _context.SaveChangesAsync();
        return Ok();
    }
}