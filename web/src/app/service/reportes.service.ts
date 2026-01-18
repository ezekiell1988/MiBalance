import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoggerService } from './logger.service';
import {
  EstadoResultados,
  FlujoCaja,
  BalanceGeneral
} from '../shared/models';

/**
 * Servicio para generar reportes financieros
 * Consume la API de MiBalance
 */
@Injectable({
  providedIn: 'root'
})
export class ReportesService {
  private readonly http = inject(HttpClient);
  private readonly logger = inject(LoggerService).getLogger('ReportesService');
  private readonly baseUrl = `${environment.apiUrl}reportes`;

  /**
   * Genera el estado de resultados para un período
   */
  getEstadoResultados(fechaDesde: Date, fechaHasta: Date): Observable<EstadoResultados> {
    this.logger.debug('Generando estado de resultados', { fechaDesde, fechaHasta });
    
    const params = new HttpParams()
      .set('fechaDesde', fechaDesde.toISOString())
      .set('fechaHasta', fechaHasta.toISOString());

    return this.http.get<EstadoResultados>(`${this.baseUrl}/estado-resultados`, { params });
  }

  /**
   * Genera el flujo de caja mensual para un año
   */
  getFlujoCaja(año: number): Observable<FlujoCaja> {
    this.logger.debug('Generando flujo de caja', { año });
    
    const params = new HttpParams().set('año', año.toString());

    return this.http.get<FlujoCaja>(`${this.baseUrl}/flujo-caja`, { params });
  }

  /**
   * Genera el balance general a una fecha específica
   */
  getBalanceGeneral(fecha: Date): Observable<BalanceGeneral> {
    this.logger.debug('Generando balance general', { fecha });
    
    const params = new HttpParams().set('fecha', fecha.toISOString());

    return this.http.get<BalanceGeneral>(`${this.baseUrl}/balance-general`, { params });
  }

  /**
   * Obtiene análisis de gastos por categoría
   */
  getGastosPorCategoria(fechaDesde: Date, fechaHasta: Date): Observable<{
    categoria: string;
    monto: number;
    porcentaje: number;
  }[]> {
    this.logger.debug('Obteniendo gastos por categoría', { fechaDesde, fechaHasta });
    
    const params = new HttpParams()
      .set('fechaDesde', fechaDesde.toISOString())
      .set('fechaHasta', fechaHasta.toISOString());

    return this.http.get<any[]>(`${this.baseUrl}/gastos-por-categoria`, { params });
  }

  /**
   * Obtiene el análisis de tendencias
   */
  getTendencias(meses: number = 6): Observable<{
    mes: string;
    ingresos: number;
    gastos: number;
    balance: number;
  }[]> {
    this.logger.debug('Obteniendo análisis de tendencias', { meses });
    
    const params = new HttpParams().set('meses', meses.toString());

    return this.http.get<any[]>(`${this.baseUrl}/tendencias`, { params });
  }
}
