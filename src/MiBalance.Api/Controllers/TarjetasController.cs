using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBalance.Core.Entities;
using MiBalance.Infrastructure.Data;

namespace MiBalance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarjetasController : ControllerBase
{
    private readonly MiBalanceDbContext _context;

    public TarjetasController(MiBalanceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todas las tarjetas
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarjeta>>> GetTarjetas()
    {
        return await _context.Tarjetas
            .Where(t => t.IsActive)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene una tarjeta por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Tarjeta>> GetTarjeta(int id)
    {
        var tarjeta = await _context.Tarjetas.FindAsync(id);

        if (tarjeta == null)
            return NotFound();

        return tarjeta;
    }

    /// <summary>
    /// Crea una nueva tarjeta
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Tarjeta>> CreateTarjeta(Tarjeta tarjeta)
    {
        _context.Tarjetas.Add(tarjeta);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTarjeta), new { id = tarjeta.Id }, tarjeta);
    }

    /// <summary>
    /// Obtiene el resumen de gastos por tarjeta en un período
    /// </summary>
    [HttpGet("{id}/resumen")]
    public async Task<ActionResult<object>> GetResumenTarjeta(
        int id,
        [FromQuery] DateTime fechaDesde,
        [FromQuery] DateTime fechaHasta)
    {
        var tarjeta = await _context.Tarjetas.FindAsync(id);
        if (tarjeta == null)
            return NotFound();

        var transacciones = await _context.Transacciones
            .Where(t => t.TarjetaId == id && t.Fecha >= fechaDesde && t.Fecha <= fechaHasta)
            .ToListAsync();

        var resumen = new
        {
            Tarjeta = tarjeta,
            TotalGastado = transacciones.Sum(t => t.Monto),
            CantidadTransacciones = transacciones.Count,
            LimiteDisponible = tarjeta.LimiteCredito - transacciones.Sum(t => t.Monto)
        };

        return Ok(resumen);
    }

    /// <summary>
    /// Obtiene el consolidado de todas las tarjetas de crédito para el mes actual
    /// </summary>
    [HttpGet("consolidado-mensual")]
    public async Task<ActionResult<object>> GetConsolidadoMensual([FromQuery] int mes, [FromQuery] int año)
    {
        var tarjetas = await _context.Tarjetas
            .Where(t => t.Tipo == TipoTarjeta.Credito && t.IsActive)
            .ToListAsync();

        var resultado = new List<object>();

        foreach (var tarjeta in tarjetas)
        {
            var fechaCorte = new DateTime(año, mes, tarjeta.DiaCorte);
            var fechaInicio = fechaCorte.AddMonths(-1);

            var gastos = await _context.Transacciones
                .Where(t => t.TarjetaId == tarjeta.Id && t.Fecha >= fechaInicio && t.Fecha < fechaCorte)
                .SumAsync(t => t.Monto);

            resultado.Add(new
            {
                TarjetaId = tarjeta.Id,
                Nombre = tarjeta.Nombre,
                Banco = tarjeta.Banco,
                UltimosDigitos = tarjeta.UltimosDigitos,
                TotalAPagar = gastos,
                FechaPago = new DateTime(año, mes, tarjeta.DiaPago),
                DiaPago = tarjeta.DiaPago
            });
        }

        return Ok(new
        {
            Mes = mes,
            Año = año,
            TotalGeneral = resultado.Sum(r => (decimal)r.GetType().GetProperty("TotalAPagar")!.GetValue(r)!),
            Tarjetas = resultado
        });
    }
}
