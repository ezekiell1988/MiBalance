using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBalance.Core.Entities;
using MiBalance.Infrastructure.Data;

namespace MiBalance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransaccionesController : ControllerBase
{
    private readonly MiBalanceDbContext _context;

    public TransaccionesController(MiBalanceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas las transacciones con filtros opcionales
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaccion>>> GetTransacciones(
        [FromQuery] DateTime? fechaDesde,
        [FromQuery] DateTime? fechaHasta,
        [FromQuery] TipoTransaccion? tipo,
        [FromQuery] int? categoriaId)
    {
        var query = _context.Transacciones
            .Include(t => t.Categoria)
            .Include(t => t.Tarjeta)
            .Include(t => t.Proveedor)
            .AsQueryable();

        if (fechaDesde.HasValue)
            query = query.Where(t => t.Fecha >= fechaDesde.Value);

        if (fechaHasta.HasValue)
            query = query.Where(t => t.Fecha <= fechaHasta.Value);

        if (tipo.HasValue)
            query = query.Where(t => t.Tipo == tipo.Value);

        if (categoriaId.HasValue)
            query = query.Where(t => t.CategoriaId == categoriaId.Value);

        return await query.OrderByDescending(t => t.Fecha).ToListAsync();
    }

    /// <summary>
    /// Obtiene una transacción por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Transaccion>> GetTransaccion(int id)
    {
        var transaccion = await _context.Transacciones
            .Include(t => t.Categoria)
            .Include(t => t.Tarjeta)
            .Include(t => t.Proveedor)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transaccion == null)
            return NotFound();

        return transaccion;
    }

    /// <summary>
    /// Crea una nueva transacción
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Transaccion>> CreateTransaccion(Transaccion transaccion)
    {
        _context.Transacciones.Add(transaccion);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTransaccion), new { id = transaccion.Id }, transaccion);
    }

    /// <summary>
    /// Actualiza una transacción existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaccion(int id, Transaccion transaccion)
    {
        if (id != transaccion.Id)
            return BadRequest();

        transaccion.UpdatedAt = DateTime.UtcNow;
        _context.Entry(transaccion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Transacciones.AnyAsync(t => t.Id == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    /// <summary>
    /// Elimina una transacción
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaccion(int id)
    {
        var transaccion = await _context.Transacciones.FindAsync(id);
        if (transaccion == null)
            return NotFound();

        _context.Transacciones.Remove(transaccion);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Obtiene el resumen de gastos por categoría
    /// </summary>
    [HttpGet("resumen/por-categoria")]
    public async Task<ActionResult<IEnumerable<object>>> GetResumenPorCategoria(
        [FromQuery] DateTime fechaDesde,
        [FromQuery] DateTime fechaHasta)
    {
        var resumen = await _context.Transacciones
            .Where(t => t.Fecha >= fechaDesde && t.Fecha <= fechaHasta && t.Tipo == TipoTransaccion.Gasto)
            .Include(t => t.Categoria)
            .GroupBy(t => new { t.CategoriaId, t.Categoria.Nombre })
            .Select(g => new
            {
                CategoriaId = g.Key.CategoriaId,
                CategoriaNombre = g.Key.Nombre,
                TotalGastado = g.Sum(t => t.Monto),
                CantidadTransacciones = g.Count()
            })
            .OrderByDescending(r => r.TotalGastado)
            .ToListAsync();

        return Ok(resumen);
    }
}
