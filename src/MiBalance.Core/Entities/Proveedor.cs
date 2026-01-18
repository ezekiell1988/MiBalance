namespace MiBalance.Core.Entities;

/// <summary>
/// Proveedores (comercios, servicios)
/// </summary>
public class Proveedor : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? RUC { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public TipoProveedor Tipo { get; set; }
    
    // Navegación
    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}

public enum TipoProveedor
{
    Comercio = 1,
    Servicio = 2,
    Financiero = 3
}
