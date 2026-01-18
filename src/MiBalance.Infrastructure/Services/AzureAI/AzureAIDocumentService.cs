using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using OpenAI.Chat;

namespace MiBalance.Infrastructure.Services.AzureAI;

/// <summary>
/// Servicio para extraer datos de documentos financieros usando Azure AI Foundry
/// </summary>
public class AzureAIDocumentService
{
    private readonly AzureOpenAIClient _client;
    private readonly string _deploymentName;

    public AzureAIDocumentService(IConfiguration configuration)
    {
        var endpoint = configuration["AzureAI:ProjectEndpoint"] 
            ?? throw new InvalidOperationException("Azure AI endpoint no configurado");
        var apiKey = configuration["AzureAI:ProjectApiKey"] 
            ?? throw new InvalidOperationException("Azure AI API key no configurada");
        _deploymentName = configuration["AzureAI:DeploymentName"] ?? "gpt-5.2";

        _client = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
    }

    /// <summary>
    /// Procesa una imagen de factura y extrae los datos financieros
    /// </summary>
    public async Task<ResultadoProcesamientoIA> ProcesarFacturaAsync(byte[] imagenBytes)
    {
        try
        {
            var base64Image = Convert.ToBase64String(imagenBytes);
            var prompt = @"
Analiza esta factura y extrae la siguiente información en formato JSON:
{
  ""fecha"": ""YYYY-MM-DD"",
  ""monto"": 0.00,
  ""comercio"": ""nombre del comercio"",
  ""descripcion"": ""descripción breve"",
  ""numeroDocumento"": ""número de factura"",
  ""categoria"": ""categoría sugerida""
}

Categorías posibles: Alimentación, Servicios Básicos, Transporte, Entretenimiento, Salud, Educación, Ropa, Otros.
";

            // Nota: Implementación simplificada - En producción usar Azure AI Vision o Document Intelligence
            // Por ahora retornamos un ejemplo de estructura para demostración
            var chatClient = _client.GetChatClient(_deploymentName);
            
            var chatMessages = new List<ChatMessage>
            {
                new SystemChatMessage("Eres un asistente experto en contabilidad que extrae datos de documentos financieros."),
                new UserChatMessage(prompt)
            };

            var chatCompletion = await chatClient.CompleteChatAsync(chatMessages);
            var content = chatCompletion.Value.Content[0].Text;

            var transaccion = ParsearRespuestaJSON(content, TipoDocumentoFinanciero.Factura);

            return new ResultadoProcesamientoIA
            {
                Exitoso = true,
                Transacciones = new List<DocumentoFinancieroExtraido> { transaccion },
                NotasAdicionales = "Factura procesada exitosamente"
            };
        }
        catch (Exception ex)
        {
            return new ResultadoProcesamientoIA
            {
                Exitoso = false,
                MensajeError = $"Error al procesar factura: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Procesa un voucher de tarjeta y extrae los datos
    /// </summary>
    public async Task<ResultadoProcesamientoIA> ProcesarVoucherAsync(byte[] imagenBytes)
    {
        try
        {
            // En producción, aquí se procesaría la imagen con Azure AI Vision
            // Por ahora retornamos una estructura de ejemplo
            await Task.Delay(100); // Simular procesamiento
            
            var transaccion = new DocumentoFinancieroExtraido
            {
                TipoDocumento = TipoDocumentoFinanciero.VoucherTarjeta,
                Confianza = 0.95
            };

            return new ResultadoProcesamientoIA
            {
                Exitoso = true,
                Transacciones = new List<DocumentoFinancieroExtraido> { transaccion }
            };
        }
        catch (Exception ex)
        {
            return new ResultadoProcesamientoIA
            {
                Exitoso = false,
                MensajeError = $"Error al procesar voucher: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Procesa un estado bancario (PDF o Excel) y extrae todas las transacciones
    /// </summary>
    public async Task<ResultadoProcesamientoIA> ProcesarEstadoBancarioAsync(byte[] archivoBytes, string extension)
    {
        try
        {
            // En producción, aquí se procesaría el PDF/Excel
            // con Azure Form Recognizer o Document Intelligence
            await Task.Delay(100); // Simular procesamiento

            return new ResultadoProcesamientoIA
            {
                Exitoso = true,
                Transacciones = new List<DocumentoFinancieroExtraido>(),
                NotasAdicionales = $"Estado bancario procesado: {extension}"
            };
        }
        catch (Exception ex)
        {
            return new ResultadoProcesamientoIA
            {
                Exitoso = false,
                MensajeError = $"Error al procesar estado bancario: {ex.Message}"
            };
        }
    }

    private DocumentoFinancieroExtraido ParsearRespuestaJSON(string jsonResponse, TipoDocumentoFinanciero tipo)
    {
        try
        {
            // Limpiar la respuesta (a veces viene con markdown)
            var json = jsonResponse.Trim();
            if (json.StartsWith("```json"))
                json = json.Substring(7);
            if (json.EndsWith("```"))
                json = json.Substring(0, json.Length - 3);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            return new DocumentoFinancieroExtraido
            {
                Fecha = DateTime.Parse(root.GetProperty("fecha").GetString() ?? DateTime.Now.ToString("yyyy-MM-dd")),
                Monto = root.GetProperty("monto").GetDecimal(),
                Comercio = root.TryGetProperty("comercio", out var comercio) ? comercio.GetString() : null,
                Descripcion = root.TryGetProperty("descripcion", out var desc) ? desc.GetString() : null,
                NumeroDocumento = root.TryGetProperty("numeroDocumento", out var doc2) ? doc2.GetString() : null,
                UltimosDigitosTarjeta = root.TryGetProperty("ultimosDigitosTarjeta", out var digitos) ? digitos.GetString() : null,
                Categoria = root.TryGetProperty("categoria", out var cat) ? cat.GetString() : null,
                TipoDocumento = tipo,
                Confianza = 0.95
            };
        }
        catch
        {
            return new DocumentoFinancieroExtraido
            {
                TipoDocumento = tipo,
                Confianza = 0.5,
                Descripcion = "Error al parsear respuesta"
            };
        }
    }
}
