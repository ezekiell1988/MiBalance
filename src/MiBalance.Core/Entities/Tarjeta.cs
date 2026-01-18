namespace MiBalance.Core.Entities;

/// <summary>
/// Tarjetas de crédito y débito
/// </summary>
public class Tarjeta : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string UltimosDigitos { get; set; } = string.Empty;
    public TipoTarjeta Tipo { get; set; }
    public string Banco { get; set; } = string.Empty;
    public decimal? LimiteCredito { get; set; }
    public int DiaCorte { get; set; }
    public int DiaPago { get; set; }
    public string? Titular { get; set; }
    
    // Navegación
    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}

public enum TipoTarjeta
{
    Credito = 1,
    Debito = 2
}
