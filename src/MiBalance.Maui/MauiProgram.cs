using Microsoft.Extensions.Logging;

namespace MiBalance.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Configurar la URL de la API
		builder.Services.AddSingleton(sp => new HttpClient
		{
			BaseAddress = new Uri("http://172.191.128.24:5000/api/")
		});

		// Registrar servicios y ViewModels
		// builder.Services.AddTransient<TransaccionesService>();
		// builder.Services.AddTransient<TransaccionesViewModel>();

		return builder.Build();
	}
}
