import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoggerService } from './logger.service';
import {
  Tarjeta,
  ResumenTarjeta,
  ConsolidadoMensual
} from '../shared/models';

/**
 * Servicio para gestionar tarjetas de crédito/débito
 * Consume la API de MiBalance
 */
@Injectable({
  providedIn: 'root'
})
export class TarjetasService {
  private readonly http = inject(HttpClient);
  private readonly logger = inject(LoggerService).getLogger('TarjetasService');
  private readonly baseUrl = `${environment.apiUrl}tarjetas`;

  /**
   * Obtiene todas las tarjetas activas
   */
  getTarjetas(): Observable<Tarjeta[]> {
    this.logger.debug('Obteniendo todas las tarjetas');
    return this.http.get<Tarjeta[]>(this.baseUrl);
  }

  /**
   * Obtiene una tarjeta por ID
   */
  getTarjeta(id: number): Observable<Tarjeta> {
    this.logger.debug('Obteniendo tarjeta', { id });
    return this.http.get<Tarjeta>(`${this.baseUrl}/${id}`);
  }

  /**
   * Crea una nueva tarjeta
   */
  createTarjeta(tarjeta: Omit<Tarjeta, 'id' | 'createdAt' | 'updatedAt' | 'isActive'>): Observable<Tarjeta> {
    this.logger.debug('Creando nueva tarjeta', tarjeta);
    return this.http.post<Tarjeta>(this.baseUrl, tarjeta);
  }

  /**
   * Actualiza una tarjeta existente
   */
  updateTarjeta(id: number, tarjeta: Partial<Tarjeta>): Observable<void> {
    this.logger.debug('Actualizando tarjeta', { id, tarjeta });
    return this.http.put<void>(`${this.baseUrl}/${id}`, tarjeta);
  }

  /**
   * Elimina (desactiva) una tarjeta
   */
  deleteTarjeta(id: number): Observable<void> {
    this.logger.debug('Eliminando tarjeta', { id });
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  /**
   * Obtiene el resumen de gastos de una tarjeta en un período
   */
  getResumenTarjeta(id: number, fechaDesde: Date, fechaHasta: Date): Observable<ResumenTarjeta> {
    this.logger.debug('Obteniendo resumen de tarjeta', { id, fechaDesde, fechaHasta });
    
    const params = new HttpParams()
      .set('fechaDesde', fechaDesde.toISOString())
      .set('fechaHasta', fechaHasta.toISOString());

    return this.http.get<ResumenTarjeta>(`${this.baseUrl}/${id}/resumen`, { params });
  }

  /**
   * Obtiene el consolidado mensual de todas las tarjetas de crédito
   */
  getConsolidadoMensual(mes: number, año: number): Observable<ConsolidadoMensual> {
    this.logger.debug('Obteniendo consolidado mensual', { mes, año });
    
    const params = new HttpParams()
      .set('mes', mes.toString())
      .set('año', año.toString());

    return this.http.get<ConsolidadoMensual>(`${this.baseUrl}/consolidado-mensual`, { params });
  }
}
