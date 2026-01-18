namespace MiBalance.Core.Entities;

/// <summary>
/// Catálogo de cuentas contables según estándar contable
/// </summary>
public class CuentaContable : BaseEntity
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public TipoCuenta Tipo { get; set; }
    public NaturalezaCuenta Naturaleza { get; set; }
    public int? CuentaPadreId { get; set; }
    public int Nivel { get; set; }
    public bool EsDeMovimiento { get; set; }
    public string? Descripcion { get; set; }
    
    // Navegación
    public CuentaContable? CuentaPadre { get; set; }
    public ICollection<CuentaContable> SubCuentas { get; set; } = new List<CuentaContable>();
    public ICollection<DetalleAsiento> DetallesAsientos { get; set; } = new List<DetalleAsiento>();
}

public enum TipoCuenta
{
    Activo = 1,
    Pasivo = 2,
    Patrimonio = 3,
    Ingreso = 4,
    Gasto = 5,
    CostoVenta = 6
}

public enum NaturalezaCuenta
{
    Deudora = 1,
    Acreedora = 2
}
