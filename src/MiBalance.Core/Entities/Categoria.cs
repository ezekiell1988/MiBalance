namespace MiBalance.Core.Entities;

/// <summary>
/// Categoría de gastos e ingresos para clasificación
/// </summary>
public class Categoria : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public TipoCategoria Tipo { get; set; }
    public string? Color { get; set; }
    public string? Icono { get; set; }
    public int? CategoriaPadreId { get; set; }
    
    // Navegación
    public Categoria? CategoriaPadre { get; set; }
    public ICollection<Categoria> SubCategorias { get; set; } = new List<Categoria>();
    public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
}

public enum TipoCategoria
{
    GastoFijo = 1,
    GastoVariable = 2,
    Ingreso = 3
}
