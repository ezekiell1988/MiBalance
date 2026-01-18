namespace MiBalance.Core.Entities;

/// <summary>
/// Recordatorios para pagos y cobros
/// </summary>
public class Recordatorio : BaseEntity
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public DateTime FechaRecordatorio { get; set; }
    public TipoRecordatorio Tipo { get; set; }
    public bool Enviado { get; set; } = false;
    public DateTime? FechaEnvio { get; set; }
    public int? TransaccionId { get; set; }
    public int? CuentaPorPagarId { get; set; }
    public int? CuentaPorCobrarId { get; set; }
    
    // Navegación
    public Transaccion? Transaccion { get; set; }
    public CuentaPorPagar? CuentaPorPagar { get; set; }
    public CuentaPorCobrar? CuentaPorCobrar { get; set; }
}

public enum TipoRecordatorio
{
    PagoTarjeta = 1,
    VencimientoCuenta = 2,
    CobroPendiente = 3,
    GastoFijo = 4
}
