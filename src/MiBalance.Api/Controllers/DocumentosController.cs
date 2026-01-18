using Microsoft.AspNetCore.Mvc;
using MiBalance.Infrastructure.Services.AzureAI;

namespace MiBalance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentosController : ControllerBase
{
    private readonly AzureAIDocumentService _aiService;

    public DocumentosController(AzureAIDocumentService aiService)
    {
        _aiService = aiService;
    }

    /// <summary>
    /// Procesa una imagen de factura y extrae los datos
    /// </summary>
    [HttpPost("procesar-factura")]
    public async Task<ActionResult<ResultadoProcesamientoIA>> ProcesarFactura(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
            return BadRequest("No se proporcionó ningún archivo");

        using var memoryStream = new MemoryStream();
        await archivo.CopyToAsync(memoryStream);
        var bytes = memoryStream.ToArray();

        var resultado = await _aiService.ProcesarFacturaAsync(bytes);

        if (!resultado.Exitoso)
            return BadRequest(resultado.MensajeError);

        return Ok(resultado);
    }

    /// <summary>
    /// Procesa una imagen de voucher de tarjeta
    /// </summary>
    [HttpPost("procesar-voucher")]
    public async Task<ActionResult<ResultadoProcesamientoIA>> ProcesarVoucher(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
            return BadRequest("No se proporcionó ningún archivo");

        using var memoryStream = new MemoryStream();
        await archivo.CopyToAsync(memoryStream);
        var bytes = memoryStream.ToArray();

        var resultado = await _aiService.ProcesarVoucherAsync(bytes);

        if (!resultado.Exitoso)
            return BadRequest(resultado.MensajeError);

        return Ok(resultado);
    }

    /// <summary>
    /// Procesa un estado bancario (PDF o Excel)
    /// </summary>
    [HttpPost("procesar-estado-bancario")]
    public async Task<ActionResult<ResultadoProcesamientoIA>> ProcesarEstadoBancario(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
            return BadRequest("No se proporcionó ningún archivo");

        var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();
        if (extension != ".pdf" && extension != ".xlsx" && extension != ".xls")
            return BadRequest("Solo se aceptan archivos PDF o Excel");

        using var memoryStream = new MemoryStream();
        await archivo.CopyToAsync(memoryStream);
        var bytes = memoryStream.ToArray();

        var resultado = await _aiService.ProcesarEstadoBancarioAsync(bytes, extension);

        if (!resultado.Exitoso)
            return BadRequest(resultado.MensajeError);

        return Ok(resultado);
    }
}
