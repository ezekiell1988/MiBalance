import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, catchError, of } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoggerService } from './logger.service';

/**
 * Interface para la configuración remota desde el backend
 */
export interface AppConfig {
  nameCompany: string;
  sloganCompany: string;
  apiVersion: string;
  features?: {
    enableAzureAI?: boolean;
    enableNotifications?: boolean;
    enableReports?: boolean;
  };
  settings?: {
    maxUploadSize?: number;
    sessionTimeout?: number;
    [key: string]: any;
  };
}

/**
 * Servicio para manejar la configuración de la aplicación
 * Carga configuración desde el backend al iniciar la app
 */
@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private readonly http = inject(HttpClient);
  private readonly logger = inject(LoggerService).getLogger('ConfigService');
  
  private config: AppConfig = {
    nameCompany: 'MiBalance',
    sloganCompany: 'Control Financiero Personal',
    apiVersion: '1.0.0'
  };
  
  private configLoaded = false;

  /**
   * Carga la configuración desde el backend
   * Este método debe ser llamado en APP_INITIALIZER
   */
  loadConfig(): Promise<AppConfig> {
    this.logger.debug('Iniciando carga de configuración remota');
    
    return new Promise((resolve, reject) => {
      // Endpoint en el backend para obtener configuración
      const configUrl = `${environment.apiUrl}configuracion`;
      
      this.http.get<AppConfig>(configUrl).pipe(
        tap(config => {
          this.logger.success('Configuración cargada exitosamente', config);
          this.config = { ...this.config, ...config };
          this.configLoaded = true;
        }),
        catchError(error => {
          this.logger.warn('Error al cargar configuración remota, usando valores por defecto', error);
          // Usar valores por defecto si falla
          this.configLoaded = true;
          return of(this.config);
        })
      ).subscribe({
        next: (config) => resolve(config),
        error: (error) => {
          // No rechazar la promesa, permitir que la app arranque con valores por defecto
          this.logger.error('Error crítico cargando configuración', error);
          resolve(this.config);
        }
      });
    });
  }

  /**
   * Obtiene la configuración actual
   */
  getConfig(): AppConfig {
    return this.config;
  }

  /**
   * Obtiene el nombre de la compañía
   */
  getCompanyName(): string {
    return this.config.nameCompany;
  }

  /**
   * Obtiene el slogan de la compañía
   */
  getCompanySlogan(): string {
    return this.config.sloganCompany;
  }

  /**
   * Obtiene la versión del API
   */
  getApiVersion(): string {
    return this.config.apiVersion;
  }

  /**
   * Verifica si la configuración ha sido cargada
   */
  isConfigLoaded(): boolean {
    return this.configLoaded;
  }

  /**
   * Verifica si una característica está habilitada
   */
  isFeatureEnabled(feature: keyof AppConfig['features']): boolean {
    return this.config.features?.[feature] ?? false;
  }

  /**
   * Obtiene un setting específico
   */
  getSetting<T = any>(key: string, defaultValue?: T): T {
    return (this.config.settings?.[key] ?? defaultValue) as T;
  }
}
