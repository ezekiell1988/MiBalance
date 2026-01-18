import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoggerService } from './logger.service';
import { Categoria } from '../shared/models';

/**
 * Servicio para gestionar categorías de gastos e ingresos
 * Consume la API de MiBalance
 */
@Injectable({
  providedIn: 'root'
})
export class CategoriasService {
  private readonly http = inject(HttpClient);
  private readonly logger = inject(LoggerService).getLogger('CategoriasService');
  private readonly baseUrl = `${environment.apiUrl}categorias`;

  /**
   * Obtiene todas las categorías activas
   */
  getCategorias(): Observable<Categoria[]> {
    this.logger.debug('Obteniendo todas las categorías');
    return this.http.get<Categoria[]>(this.baseUrl);
  }

  /**
   * Obtiene una categoría por ID
   */
  getCategoria(id: number): Observable<Categoria> {
    this.logger.debug('Obteniendo categoría', { id });
    return this.http.get<Categoria>(`${this.baseUrl}/${id}`);
  }

  /**
   * Crea una nueva categoría
   */
  createCategoria(categoria: Omit<Categoria, 'id' | 'createdAt' | 'updatedAt' | 'isActive'>): Observable<Categoria> {
    this.logger.debug('Creando nueva categoría', categoria);
    return this.http.post<Categoria>(this.baseUrl, categoria);
  }

  /**
   * Actualiza una categoría existente
   */
  updateCategoria(id: number, categoria: Partial<Categoria>): Observable<void> {
    this.logger.debug('Actualizando categoría', { id, categoria });
    return this.http.put<void>(`${this.baseUrl}/${id}`, categoria);
  }

  /**
   * Elimina (desactiva) una categoría
   */
  deleteCategoria(id: number): Observable<void> {
    this.logger.debug('Eliminando categoría', { id });
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  /**
   * Obtiene categorías por tipo
   */
  getCategoriasPorTipo(tipo: number): Observable<Categoria[]> {
    this.logger.debug('Obteniendo categorías por tipo', { tipo });
    return this.http.get<Categoria[]>(`${this.baseUrl}/tipo/${tipo}`);
  }
}
