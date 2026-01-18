import { ApplicationConfig, provideZoneChangeDetection, provideAppInitializer, inject } from '@angular/core';
import { provideRouter, withHashLocation } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { Title } from '@angular/platform-browser';
import { provideIonicAngular } from '@ionic/angular/standalone';
import { routes } from './app.routes';
import { AppSettings } from './service/app-settings.service';
import { AppVariablesService } from './service/app-variables.service';
import { AppMenuService } from './service/app-menus.service';
import { PlatformDetectorService } from './service/platform-detector.service';
import { AuthService, LoggerService, ConfigService } from './service';
import { authInterceptor } from './shared/interceptors';
import { environment } from '../environments/environment';

/**
 * Factory para inicializar la configuración remota ANTES de que arranque la aplicación
 * Este es el patrón recomendado por Angular para inicialización asíncrona
 * También valida y refresca el token de autenticación si está próximo a expirar
 */
function initializeAppConfig(
  configService: ConfigService, 
  authService: AuthService, 
  loggerService: LoggerService
): () => Promise<void> {
  return async () => {
    const logger = loggerService.getLogger('AppInitializer');
    
    // 1. Cargar configuración remota desde el backend de .NET
    try {
      logger.debug('Iniciando carga de configuración del backend...');
      const config = await configService.loadConfig();
      logger.success('Configuración cargada:', {
        company: config.nameCompany,
        version: config.apiVersion
      });
    } catch (error) {
      logger.error('Error al cargar configuración (continuando con valores por defecto):', error);
    }

    // 2. Validar y refrescar token de autenticación si es necesario
    try {
      const token = authService.getToken();
      
      if (token) {
        logger.debug('Token detectado en inicialización');
        
        // Si el token está expirado, limpiar sesión
        if (authService.isTokenExpired()) {
          logger.warn('Token expirado - limpiando sesión');
          authService.clearSession();
          return;
        }
        
        // Si el token está próximo a expirar, intentar refrescarlo
        if (authService.isTokenExpiringSoon()) {
          logger.debug('Token próximo a expirar - intentando refrescar');
          
          try {
            // Convertir Observable a Promise para usar en async/await
            const refreshResponse = await new Promise<any>((resolve, reject) => {
              authService.refreshToken().subscribe({
                next: (response) => resolve(response),
                error: (error) => reject(error)
              });
            });
            
            if (refreshResponse?.success) {
              logger.success('Token refrescado exitosamente en inicialización');
            }
          } catch (refreshError) {
            logger.error('Error al refrescar token en inicialización:', refreshError);
            // No limpiar sesión aquí - dejar que el interceptor lo maneje
          }
        } else {
          logger.debug('Token válido y vigente');
        }
      }
    } catch (error) {
      logger.error('Error validando token en inicialización:', error);
      // No bloquear la carga de la app por errores de token
    }
  };
}

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withHashLocation()),
    provideAnimations(),
    provideHttpClient(withInterceptors([authInterceptor])),
    provideIonicAngular({
      // Configuración de Ionic
      mode: 'ios', // Usar estilo iOS para consistencia
      animated: true,
      // Configurar la ruta de assets de Ionicons
      innerHTMLTemplatesEnabled: true,
      // Los iconos se cargarán desde CDN de Ionicons
    }),
    Title,
    AppSettings,
    AppVariablesService,
    AppMenuService,
    PlatformDetectorService,
    AuthService,
    ConfigService, // Nuevo servicio de configuración
    // provideAppInitializer: Forma moderna (no deprecada) de APP_INITIALIZER
    // Se ejecuta ANTES de que la app arranque
    // IMPORTANTE: Usar inject() para obtener la instancia singleton del servicio
    provideAppInitializer(() => {
      const configService = inject(ConfigService);
      const authService = inject(AuthService);
      const loggerService = inject(LoggerService);
      return initializeAppConfig(configService, authService, loggerService)();
    })
  ]
};
