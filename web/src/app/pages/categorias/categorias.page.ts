import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelComponent } from '../../components/panel/panel.component';
import { LoggerService } from '../../service';

@Component({
  selector: 'app-categorias',
  standalone: true,
  imports: [CommonModule, PanelComponent],
  templateUrl: './categorias.page.html',
  styleUrls: ['./categorias.page.scss']
})
export class CategoriasPage implements OnInit {
  private readonly logger = inject(LoggerService).getLogger('CategoriasPage');

  categorias: any[] = [];

  ngOnInit() {
    this.logger.info('Página de categorías inicializada');
    this.cargarCategorias();
  }

  cargarCategorias() {
    // TODO: Conectar con API
    this.categorias = [
      { id: 1, nombre: 'Alimentación', icono: 'utensils', color: '#ff6b6b', tipo: 'Gasto' },
      { id: 2, nombre: 'Transporte', icono: 'car', color: '#4ecdc4', tipo: 'Gasto' },
      { id: 3, nombre: 'Servicios', icono: 'lightbulb', color: '#ffe66d', tipo: 'Gasto' },
      { id: 4, nombre: 'Entretenimiento', icono: 'film', color: '#a8e6cf', tipo: 'Gasto' },
      { id: 5, nombre: 'Salud', icono: 'heartbeat', color: '#ff8b94', tipo: 'Gasto' },
      { id: 6, nombre: 'Ingresos', icono: 'dollar-sign', color: '#95e1d3', tipo: 'Ingreso' },
      { id: 7, nombre: 'Educación', icono: 'graduation-cap', color: '#c7ceea', tipo: 'Gasto' },
      { id: 8, nombre: 'Otros', icono: 'ellipsis-h', color: '#b0b0b0', tipo: 'Gasto' }
    ];
  }
}
