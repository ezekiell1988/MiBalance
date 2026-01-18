namespace MiBalance.Core.Entities;

/// <summary>
/// Clientes (para cuentas por cobrar - préstamos)
/// </summary>
public class Cliente : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Identificacion { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Relacion { get; set; }
    
    // Navegación
    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
    public ICollection<CuentaPorCobrar> CuentasPorCobrar { get; set; } = new List<CuentaPorCobrar>();
}
