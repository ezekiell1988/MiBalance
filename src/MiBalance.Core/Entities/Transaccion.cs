namespace MiBalance.Core.Entities;

/// <summary>
/// Transacción financiera (gasto, ingreso, transferencia)
/// </summary>
public class Transaccion : BaseEntity
{
    public DateTime Fecha { get; set; }
    public TipoTransaccion Tipo { get; set; }
    public decimal Monto { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public int? TarjetaId { get; set; }
    public int? ProveedorId { get; set; }
    public int? ClienteId { get; set; }
    public string? NumeroDocumento { get; set; }
    public EstadoTransaccion Estado { get; set; } = EstadoTransaccion.Pendiente;
    public OrigenDatos Origen { get; set; }
    public string? NotasIA { get; set; }
    
    // Navegación
    public Categoria Categoria { get; set; } = null!;
    public Tarjeta? Tarjeta { get; set; }
    public Proveedor? Proveedor { get; set; }
    public Cliente? Cliente { get; set; }
    public AsientoContable? AsientoContable { get; set; }
    public ICollection<Recordatorio> Recordatorios { get; set; } = new List<Recordatorio>();
}

public enum TipoTransaccion
{
    Ingreso = 1,
    Gasto = 2,
    Transferencia = 3
}

public enum EstadoTransaccion
{
    Pendiente = 1,
    Completada = 2,
    Cancelada = 3
}

public enum OrigenDatos
{
    Manual = 1,
    FacturaIA = 2,
    VoucherIA = 3,
    EstadoBancarioIA = 4
}
