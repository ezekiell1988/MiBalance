namespace MiBalance.Core.Entities;

/// <summary>
/// Cuentas por cobrar (préstamos a amigos/familiares)
/// </summary>
public class CuentaPorCobrar : BaseEntity
{
    public int ClienteId { get; set; }
    public DateTime FechaPrestamo { get; set; }
    public decimal MontoOriginal { get; set; }
    public decimal MontoPendiente { get; set; }
    public DateTime? FechaVencimiento { get; set; }
    public decimal? TasaInteres { get; set; }
    public string? Observaciones { get; set; }
    public EstadoCuenta Estado { get; set; } = EstadoCuenta.Vigente;
    
    // Navegación
    public Cliente Cliente { get; set; } = null!;
    public ICollection<PagoCuenta> Pagos { get; set; } = new List<PagoCuenta>();
}

/// <summary>
/// Cuentas por pagar (tarjetas de crédito, obligaciones)
/// </summary>
public class CuentaPorPagar : BaseEntity
{
    public int? TarjetaId { get; set; }
    public int? ProveedorId { get; set; }
    public DateTime FechaEmision { get; set; }
    public decimal MontoOriginal { get; set; }
    public decimal MontoPendiente { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public decimal? InteresesMora { get; set; }
    public string? NumeroFactura { get; set; }
    public EstadoCuenta Estado { get; set; } = EstadoCuenta.Vigente;
    
    // Navegación
    public Tarjeta? Tarjeta { get; set; }
    public Proveedor? Proveedor { get; set; }
    public ICollection<PagoCuenta> Pagos { get; set; } = new List<PagoCuenta>();
}

/// <summary>
/// Registro de pagos realizados a cuentas por cobrar/pagar
/// </summary>
public class PagoCuenta : BaseEntity
{
    public int? CuentaPorCobrарId { get; set; }
    public int? CuentaPorPagarId { get; set; }
    public DateTime FechaPago { get; set; }
    public decimal Monto { get; set; }
    public string? Referencia { get; set; }
    public int? TransaccionId { get; set; }
    
    // Navegación
    public CuentaPorCobrar? CuentaPorCobrar { get; set; }
    public CuentaPorPagar? CuentaPorPagar { get; set; }
    public Transaccion? Transaccion { get; set; }
}

public enum EstadoCuenta
{
    Vigente = 1,
    Vencida = 2,
    Pagada = 3,
    Cancelada = 4
}
