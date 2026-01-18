namespace MiBalance.Infrastructure.Services.AzureAI;

/// <summary>
/// Modelo de datos extraídos de documentos financieros
/// </summary>
public class DocumentoFinancieroExtraido
{
    public DateTime Fecha { get; set; }
    public decimal Monto { get; set; }
    public string? Comercio { get; set; }
    public string? Descripcion { get; set; }
    public string? NumeroDocumento { get; set; }
    public string? UltimosDigitosTarjeta { get; set; }
    public string? Categoria { get; set; }
    public TipoDocumentoFinanciero TipoDocumento { get; set; }
    public double Confianza { get; set; }
}

public enum TipoDocumentoFinanciero
{
    Factura = 1,
    VoucherTarjeta = 2,
    EstadoBancario = 3
}

/// <summary>
/// Resultado del procesamiento de documentos con IA
/// </summary>
public class ResultadoProcesamientoIA
{
    public bool Exitoso { get; set; }
    public string? MensajeError { get; set; }
    public List<DocumentoFinancieroExtraido> Transacciones { get; set; } = new();
    public string? NotasAdicionales { get; set; }
}
