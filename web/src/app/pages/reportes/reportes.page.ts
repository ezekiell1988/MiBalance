import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelComponent } from '../../components/panel/panel.component';
import { LoggerService, ReportesService } from '../../service';
import { EstadoResultados, FlujoCaja } from '../../shared/models';

@Component({
  selector: 'app-reportes',
  standalone: true,
  imports: [CommonModule, PanelComponent],
  templateUrl: './reportes.page.html',
  styleUrls: ['./reportes.page.scss']
})
export class ReportesPage implements OnInit {
  private readonly logger = inject(LoggerService).getLogger('ReportesPage');
  private readonly reportesService = inject(ReportesService);

  estadoResultados: EstadoResultados | null = null;
  flujoCaja: FlujoCaja | null = null;
  cargando = false;
  error: string | null = null;

  ngOnInit() {
    this.logger.info('Página de reportes inicializada');
    this.cargarReportes();
  }

  cargarReportes() {
    this.logger.debug('Cargando reportes desde API');
    this.cargando = true;
    this.error = null;

    // Cargar estado de resultados del mes actual
    const hoy = new Date();
    const primerDiaMes = new Date(hoy.getFullYear(), hoy.getMonth(), 1);
    const ultimoDiaMes = new Date(hoy.getFullYear(), hoy.getMonth() + 1, 0);

    this.reportesService.getEstadoResultados(primerDiaMes, ultimoDiaMes).subscribe({
      next: (estado) => {
        this.estadoResultados = estado;
        this.logger.success('Estado de resultados cargado');
      },
      error: (err) => {
        this.error = 'Error al cargar el estado de resultados';
        this.logger.error('Error al cargar estado de resultados', err);
      }
    });

    // Cargar flujo de caja del año actual
    this.reportesService.getFlujoCaja(hoy.getFullYear()).subscribe({
      next: (flujo) => {
        this.flujoCaja = flujo;
        this.cargando = false;
        this.logger.success('Flujo de caja cargado');
      },
      error: (err) => {
        this.error = 'Error al cargar el flujo de caja';
        this.cargando = false;
        this.logger.error('Error al cargar flujo de caja', err);
      }
    });
  }
}
