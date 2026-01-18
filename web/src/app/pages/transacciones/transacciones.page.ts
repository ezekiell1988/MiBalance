import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PanelComponent } from '../../components/panel/panel.component';
import { LoggerService } from '../../service';

@Component({
  selector: 'app-transacciones',
  standalone: true,
  imports: [CommonModule, FormsModule, PanelComponent],
  templateUrl: './transacciones.page.html',
  styleUrls: ['./transacciones.page.scss']
})
export class TransaccionesPage implements OnInit {
  private readonly logger = inject(LoggerService).getLogger('TransaccionesPage');

  transacciones: any[] = [];
  filtroCategoria: string = '';
  filtroTipo: string = '';
  busqueda: string = '';

  ngOnInit() {
    this.logger.info('Página de transacciones inicializada');
    this.cargarTransacciones();
  }

  cargarTransacciones() {
    this.logger.debug('Cargando transacciones');
    
    // TODO: Conectar con el servicio real del backend
    // Datos de ejemplo
    this.transacciones = [
      {
        id: 1,
        fecha: new Date('2026-01-15'),
        descripcion: 'Supermercado La Colonia',
        categoria: 'Alimentación',
        monto: -15000,
        tipo: 'Gasto',
        tarjeta: 'Visa ****1234'
      },
      {
        id: 2,
        fecha: new Date('2026-01-14'),
        descripcion: 'Salario Enero',
        categoria: 'Ingresos',
        monto: 500000,
        tipo: 'Ingreso',
        tarjeta: null
      },
      {
        id: 3,
        fecha: new Date('2026-01-13'),
        descripcion: 'Gasolina Servicio Nacional',
        categoria: 'Transporte',
        monto: -8000,
        tipo: 'Gasto',
        tarjeta: 'Mastercard ****5678'
      },
      {
        id: 4,
        fecha: new Date('2026-01-12'),
        descripcion: 'Netflix Subscripción',
        categoria: 'Entretenimiento',
        monto: -5000,
        tipo: 'Gasto',
        tarjeta: 'Visa ****1234'
      },
      {
        id: 5,
        fecha: new Date('2026-01-10'),
        descripcion: 'Recibo de Agua',
        categoria: 'Servicios',
        monto: -7500,
        tipo: 'Gasto',
        tarjeta: null
      }
    ];
  }

  get transaccionesFiltradas() {
    return this.transacciones.filter(t => {
      const matchCategoria = !this.filtroCategoria || t.categoria === this.filtroCategoria;
      const matchTipo = !this.filtroTipo || t.tipo === this.filtroTipo;
      const matchBusqueda = !this.busqueda || 
        t.descripcion.toLowerCase().includes(this.busqueda.toLowerCase());
      
      return matchCategoria && matchTipo && matchBusqueda;
    });
  }

  get categorias(): string[] {
    return [...new Set(this.transacciones.map(t => t.categoria))];
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
    // TODO: Implementar eliminación con confirmación
  }

  limpiarFiltros() {
    this.filtroCategoria = '';
    this.filtroTipo = '';
    this.busqueda = '';
  }

  get totalIngresos(): number {
    return this.transaccionesFiltradas
      .filter(t => t.monto > 0)
      .reduce((sum, t) => sum + t.monto, 0);
  }

  get totalGastos(): number {
    return this.transaccionesFiltradas
      .filter(t => t.monto < 0)
      .reduce((sum, t) => sum + Math.abs(t.monto), 0);
  }

  get balance(): number {
    return this.totalIngresos - this.totalGastos;
  }
}
