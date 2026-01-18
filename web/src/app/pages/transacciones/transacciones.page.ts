import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PanelComponent } from '../../components/panel/panel.component';
import { LoggerService, TransaccionesService } from '../../service';
import { Transaccion, TipoTransaccion } from '../../shared/models';

@Component({
  selector: 'app-transacciones',
  standalone: true,
  imports: [CommonModule, FormsModule, PanelComponent],
  templateUrl: './transacciones.page.html',
  styleUrls: ['./transacciones.page.scss']
})
export class TransaccionesPage implements OnInit {
  private readonly logger = inject(LoggerService).getLogger('TransaccionesPage');
  private readonly transaccionesService = inject(TransaccionesService);

  transacciones: Transaccion[] = [];
  filtroCategoria: number | null = null;
  filtroTipo: TipoTransaccion | null = null;
  busqueda: string = '';
  cargando = false;
  error: string | null = null;

  ngOnInit() {
    this.logger.info('Página de transacciones inicializada');
    this.cargarTransacciones();
  }

  cargarTransacciones() {
    this.logger.debug('Cargando transacciones desde API');
    this.cargando = true;
    this.error = null;
    
    const filtros = {
      categoriaId: this.filtroCategoria || undefined,
      tipo: this.filtroTipo || undefined
    };
    
    this.transaccionesService.getTransacciones(filtros).subscribe({
      next: (transacciones) => {
        this.transacciones = transacciones;
        this.cargando = false;
        this.logger.success(`${transacciones.length} transacciones cargadas`);
      },
      error: (err) => {
        this.error = 'Error al cargar las transacciones';
        this.cargando = false;
        this.logger.error('Error al cargar transacciones', err);
      }
    });
  }

  get transaccionesFiltradas() {
    return this.transacciones.filter(t => {
      const matchBusqueda = !this.busqueda || 
        t.descripcion.toLowerCase().includes(this.busqueda.toLowerCase());
      
      return matchBusqueda;
    });
  }

  get categorias(): Set<number> {
    return new Set(this.transacciones.map(t => t.categoriaId));
  }

  agregarTransaccion() {
    this.logger.info('Agregar nueva transacción');
    // TODO: Implementar modal o navegación para agregar transacción
  }

  editarTransaccion(id: number) {
    this.logger.debug('Editar transacción:', id);
    // TODO: Implementar edición
  }

  eliminarTransaccion(id: number) {
    this.logger.debug('Eliminar transacción:', id);
    if (confirm('¿Está seguro de eliminar esta transacción?')) {
      this.transaccionesService.deleteTransaccion(id).subscribe({
        next: () => {
          this.logger.success('Transacción eliminada');
          this.cargarTransacciones();
        },
        error: (err) => {
          this.logger.error('Error al eliminar transacción', err);
        }
      });
    }
  }

  limpiarFiltros() {
    this.filtroCategoria = null;
    this.filtroTipo = null;
    this.busqueda = '';
    this.cargarTransacciones();
  }

  aplicarFiltros() {
    this.cargarTransacciones();
  }

  get totalIngresos(): number {
    return this.transaccionesFiltradas
      .filter(t => t.tipo === TipoTransaccion.Ingreso)
      .reduce((sum, t) => sum + t.monto, 0);
  }

  get totalGastos(): number {
    return this.transaccionesFiltradas
      .filter(t => t.tipo === TipoTransaccion.Gasto)
      .reduce((sum, t) => sum + t.monto, 0);
  }

  get balance(): number {
    return this.totalIngresos - this.totalGastos;
  }
}
