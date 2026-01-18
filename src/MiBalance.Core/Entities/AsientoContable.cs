namespace MiBalance.Core.Entities;

/// <summary>
/// Asiento contable (entrada en el libro diario)
/// </summary>
public class AsientoContable : BaseEntity
{
    public DateTime Fecha { get; set; }
    public string Numero { get; set; } = string.Empty;
    public string Concepto { get; set; } = string.Empty;
    public TipoAsiento Tipo { get; set; }
    public EstadoAsiento Estado { get; set; } = EstadoAsiento.Borrador;
    public string? Observaciones { get; set; }
    public int? TransaccionId { get; set; }
    
    // Navegación
    public Transaccion? Transaccion { get; set; }
    public ICollection<DetalleAsiento> Detalles { get; set; } = new List<DetalleAsiento>();
}

/// <summary>
/// Detalle del asiento contable (debe y haber)
/// </summary>
public class DetalleAsiento : BaseEntity
{
    public int AsientoContableId { get; set; }
    public int CuentaContableId { get; set; }
    public decimal Debe { get; set; }
    public decimal Haber { get; set; }
    public string? Descripcion { get; set; }
    
    // Navegación
    public AsientoContable AsientoContable { get; set; } = null!;
    public CuentaContable CuentaContable { get; set; } = null!;
}

public enum TipoAsiento
{
    Apertura = 1,
    Diario = 2,
    Ajuste = 3,
    Cierre = 4
}

public enum EstadoAsiento
{
    Borrador = 1,
    Contabilizado = 2,
    Anulado = 3
}
