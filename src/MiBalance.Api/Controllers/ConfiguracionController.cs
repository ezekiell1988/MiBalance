using Microsoft.AspNetCore.Mvc;

namespace MiBalance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfiguracionController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ConfiguracionController> _logger;

    public ConfiguracionController(IConfiguration configuration, ILogger<ConfiguracionController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene la configuración inicial de la aplicación
    /// </summary>
    /// <returns>Configuración de la aplicación</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ConfiguracionResponse), StatusCodes.Status200OK)]
    public IActionResult ObtenerConfiguracion()
    {
        try
        {
            var config = new ConfiguracionResponse
            {
                NameCompany = "MiBalance",
                SloganCompany = "Control Financiero Personal",
                ApiVersion = "1.0.0",
                Features = new Features
                {
                    EnableAzureAI = !string.IsNullOrEmpty(_configuration["AzureAI:Endpoint"]),
                    EnableNotifications = true,
                    EnableReports = true
                },
                Settings = new Settings
                {
                    MaxUploadSize = 10485760, // 10MB
                    SessionTimeout = 30 // 30 minutos
                }
            };

            _logger.LogInformation("Configuración enviada al cliente");
            return Ok(config);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la configuración");
            return StatusCode(500, new { error = "Error al obtener la configuración" });
        }
    }
}

public class ConfiguracionResponse
{
    public string NameCompany { get; set; } = string.Empty;
    public string SloganCompany { get; set; } = string.Empty;
    public string ApiVersion { get; set; } = string.Empty;
    public Features Features { get; set; } = new();
    public Settings Settings { get; set; } = new();
}

public class Features
{
    public bool EnableAzureAI { get; set; }
    public bool EnableNotifications { get; set; }
    public bool EnableReports { get; set; }
}

public class Settings
{
    public long MaxUploadSize { get; set; }
    public int SessionTimeout { get; set; }
}
