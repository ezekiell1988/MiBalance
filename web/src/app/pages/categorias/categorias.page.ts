import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelComponent } from '../../components/panel/panel.component';
import { LoggerService, CategoriasService } from '../../service';
import { Categoria } from '../../shared/models';

@Component({
  selector: 'app-categorias',
  standalone: true,
  imports: [CommonModule, PanelComponent],
  templateUrl: './categorias.page.html',
  styleUrls: ['./categorias.page.scss']
})
export class CategoriasPage implements OnInit {
  private readonly logger = inject(LoggerService).getLogger('CategoriasPage');
  private readonly categoriasService = inject(CategoriasService);

  categorias: Categoria[] = [];
  cargando = false;
  error: string | null = null;

  ngOnInit() {
    this.logger.info('Página de categorías inicializada');
    this.cargarCategorias();
  }

  cargarCategorias() {
    this.logger.debug('Cargando categorías desde API');
    this.cargando = true;
    this.error = null;
    
    this.categoriasService.getCategorias().subscribe({
      next: (categorias) => {
        this.categorias = categorias;
        this.cargando = false;
        this.logger.success(`${categorias.length} categorías cargadas`);
      },
      error: (err) => {
        this.error = 'Error al cargar las categorías';
        this.cargando = false;
        this.logger.error('Error al cargar categorías', err);
      }
    });
  }
}
