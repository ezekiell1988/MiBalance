using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBalance.Infrastructure.Data;

namespace MiBalance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly MiBalanceDbContext _context;

    public ReportesController(MiBalanceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Genera el estado de resultados para un período
    /// </summary>
    [HttpGet("estado-resultados")]
    public async Task<ActionResult<object>> GetEstadoResultados(
        [FromQuery] DateTime fechaDesde,
        [FromQuery] DateTime fechaHasta)
    {
        var ingresos = await _context.Transacciones
            .Where(t => t.Fecha >= fechaDesde && t.Fecha <= fechaHasta && t.Tipo == Core.Entities.TipoTransaccion.Ingreso)
            .SumAsync(t => t.Monto);

        var gastos = await _context.Transacciones
            .Where(t => t.Fecha >= fechaDesde && t.Fecha <= fechaHasta && t.Tipo == Core.Entities.TipoTransaccion.Gasto)
            .SumAsync(t => t.Monto);

        var resultado = new
        {
            Periodo = new { FechaDesde = fechaDesde, FechaHasta = fechaHasta },
            TotalIngresos = ingresos,
            TotalGastos = gastos,
            ResultadoNeto = ingresos - gastos
        };

        return Ok(resultado);
    }

    /// <summary>
    /// Genera el flujo de caja mensual
    /// </summary>
    [HttpGet("flujo-caja")]
    public async Task<ActionResult<object>> GetFlujoCaja([FromQuery] int año)
    {
        var flujo = new List<object>();

        for (int mes = 1; mes <= 12; mes++)
        {
            var fechaInicio = new DateTime(año, mes, 1);
            var fechaFin = fechaInicio.AddMonths(1).AddDays(-1);

            var ingresos = await _context.Transacciones
                .Where(t => t.Fecha >= fechaInicio && t.Fecha <= fechaFin && t.Tipo == Core.Entities.TipoTransaccion.Ingreso)
                .SumAsync(t => t.Monto);

            var gastos = await _context.Transacciones
                .Where(t => t.Fecha >= fechaInicio && t.Fecha <= fechaFin && t.Tipo == Core.Entities.TipoTransaccion.Gasto)
                .SumAsync(t => t.Monto);

            flujo.Add(new
            {
                Mes = mes,
                NombreMes = fechaInicio.ToString("MMMM"),
                Ingresos = ingresos,
                Gastos = gastos,
                Saldo = ingresos - gastos
            });
        }

        return Ok(new { Año = año, Meses = flujo });
    }

    /// <summary>
    /// Genera el balance general
    /// </summary>
    [HttpGet("balance-general")]
    public async Task<ActionResult<object>> GetBalanceGeneral([FromQuery] DateTime fecha)
    {
        // Este es un ejemplo simplificado
        // En un sistema real, se calcularía desde los asientos contables

        var cuentasPorCobrar = await _context.CuentasPorCobrar
            .Where(c => c.Estado == Core.Entities.EstadoCuenta.Vigente)
            .SumAsync(c => c.MontoPendiente);

        var cuentasPorPagar = await _context.CuentasPorPagar
            .Where(c => c.Estado == Core.Entities.EstadoCuenta.Vigente)
            .SumAsync(c => c.MontoPendiente);

        var balance = new
        {
            Fecha = fecha,
            Activos = new
            {
                Corriente = new
                {
                    CuentasPorCobrar = cuentasPorCobrar
                },
                Total = cuentasPorCobrar
            },
            Pasivos = new
            {
                Corriente = new
                {
                    CuentasPorPagar = cuentasPorPagar
                },
                Total = cuentasPorPagar
            },
            Patrimonio = cuentasPorCobrar - cuentasPorPagar
        };

        return Ok(balance);
    }

    /// <summary>
    /// Obtiene tendencias de gastos por mes
    /// </summary>
    [HttpGet("tendencias")]
    public async Task<ActionResult<object>> GetTendencias([FromQuery] int meses = 6)
    {
        var fechaHasta = DateTime.Now;
        var fechaDesde = fechaHasta.AddMonths(-meses);

        var tendencias = await _context.Transacciones
            .Where(t => t.Fecha >= fechaDesde && t.Fecha <= fechaHasta && t.Tipo == Core.Entities.TipoTransaccion.Gasto)
            .Include(t => t.Categoria)
            .GroupBy(t => new { Año = t.Fecha.Year, Mes = t.Fecha.Month, t.Categoria.Nombre })
            .Select(g => new
            {
                Año = g.Key.Año,
                Mes = g.Key.Mes,
                Categoria = g.Key.Nombre,
                Total = g.Sum(t => t.Monto)
            })
            .OrderBy(r => r.Año)
            .ThenBy(r => r.Mes)
            .ToListAsync();

        return Ok(tendencias);
    }
}
