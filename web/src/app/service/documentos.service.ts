import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoggerService } from './logger.service';
import { ResultadoProcesamientoIA } from '../shared/models';

/**
 * Servicio para procesar documentos con Azure AI
 * Consume la API de MiBalance
 */
@Injectable({
  providedIn: 'root'
})
export class DocumentosService {
  private readonly http = inject(HttpClient);
  private readonly logger = inject(LoggerService).getLogger('DocumentosService');
  private readonly baseUrl = `${environment.apiUrl}documentos`;

  /**
   * Procesa una imagen de factura y extrae los datos
   */
  procesarFactura(archivo: File): Observable<ResultadoProcesamientoIA> {
    this.logger.debug('Procesando factura', { nombreArchivo: archivo.name });
    
    const formData = new FormData();
    formData.append('archivo', archivo, archivo.name);

    return this.http.post<ResultadoProcesamientoIA>(
      `${this.baseUrl}/procesar-factura`,
      formData
    );
  }

  /**
   * Procesa una imagen de voucher de tarjeta
   */
  procesarVoucher(archivo: File): Observable<ResultadoProcesamientoIA> {
    this.logger.debug('Procesando voucher', { nombreArchivo: archivo.name });
    
    const formData = new FormData();
    formData.append('archivo', archivo, archivo.name);

    return this.http.post<ResultadoProcesamientoIA>(
      `${this.baseUrl}/procesar-voucher`,
      formData
    );
  }

  /**
   * Procesa un estado bancario (PDF o Excel)
   */
  procesarEstadoBancario(archivo: File): Observable<ResultadoProcesamientoIA> {
    this.logger.debug('Procesando estado bancario', { nombreArchivo: archivo.name });
    
    const formData = new FormData();
    formData.append('archivo', archivo, archivo.name);

    return this.http.post<ResultadoProcesamientoIA>(
      `${this.baseUrl}/procesar-estado-bancario`,
      formData
    );
  }
}
