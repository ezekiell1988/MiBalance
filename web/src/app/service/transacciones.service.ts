import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoggerService } from './logger.service';
import {
  Transaccion,
  TransaccionCreate,
  TransaccionUpdate,
  TransaccionFiltros
} from '../shared/models';

/**
 * Servicio para gestionar transacciones financieras
 * Consume la API de MiBalance
 */
@Injectable({
  providedIn: 'root'
})
export class TransaccionesService {
  private readonly http = inject(HttpClient);
  private readonly logger = inject(LoggerService).getLogger('TransaccionesService');
  private readonly baseUrl = `${environment.apiUrl}transacciones`;

  /**
   * Obtiene todas las transacciones con filtros opcionales
   */
  getTransacciones(filtros?: TransaccionFiltros): Observable<Transaccion[]> {
    this.logger.debug('Obteniendo transacciones', { filtros });
    
    let params = new HttpParams();
    
    if (filtros) {
      if (filtros.fechaDesde) {
        const fecha = typeof filtros.fechaDesde === 'string' 
          ? filtros.fechaDesde 
          : filtros.fechaDesde.toISOString();
        params = params.set('fechaDesde', fecha);
      }
      if (filtros.fechaHasta) {
        const fecha = typeof filtros.fechaHasta === 'string' 
          ? filtros.fechaHasta 
          : filtros.fechaHasta.toISOString();
        params = params.set('fechaHasta', fecha);
      }
      if (filtros.tipo !== undefined) {
        params = params.set('tipo', filtros.tipo.toString());
      }
      if (filtros.categoriaId !== undefined) {
        params = params.set('categoriaId', filtros.categoriaId.toString());
      }
    }

    return this.http.get<Transaccion[]>(this.baseUrl, { params });
  }

  /**
   * Obtiene una transacción por ID
   */
  getTransaccion(id: number): Observable<Transaccion> {
    this.logger.debug('Obteniendo transacción', { id });
    return this.http.get<Transaccion>(`${this.baseUrl}/${id}`);
  }

  /**
   * Crea una nueva transacción
   */
  createTransaccion(transaccion: TransaccionCreate): Observable<Transaccion> {
    this.logger.debug('Creando nueva transacción', transaccion);
    return this.http.post<Transaccion>(this.baseUrl, transaccion);
  }

  /**
   * Actualiza una transacción existente
   */
  updateTransaccion(id: number, transaccion: Partial<TransaccionUpdate>): Observable<void> {
    this.logger.debug('Actualizando transacción', { id, transaccion });
    return this.http.put<void>(`${this.baseUrl}/${id}`, { ...transaccion, id });
  }

  /**
   * Elimina (desactiva) una transacción
   */
  deleteTransaccion(id: number): Observable<void> {
    this.logger.debug('Eliminando transacción', { id });
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  /**
   * Obtiene el resumen de transacciones del mes actual
   */
  getResumenMesActual(): Observable<{
    totalIngresos: number;
    totalGastos: number;
    balance: number;
    transaccionesPendientes: number;
  }> {
    this.logger.debug('Obteniendo resumen del mes actual');
    return this.http.get<any>(`${this.baseUrl}/resumen-mes-actual`);
  }
}
