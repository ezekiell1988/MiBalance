import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelComponent } from '../../components/panel/panel.component';
import { LoggerService, TarjetasService } from '../../service';
import { Tarjeta } from '../../shared/models';

@Component({
  selector: 'app-tarjetas',
  standalone: true,
  imports: [CommonModule, PanelComponent],
  templateUrl: './tarjetas.page.html',
  styleUrls: ['./tarjetas.page.scss']
})
export class TarjetasPage implements OnInit {
  private readonly logger = inject(LoggerService).getLogger('TarjetasPage');
  private readonly tarjetasService = inject(TarjetasService);

  tarjetas: Tarjeta[] = [];
  cargando = false;
  error: string | null = null;

  ngOnInit() {
    this.logger.info('Página de tarjetas inicializada');
    this.cargarTarjetas();
  }

  cargarTarjetas() {
    this.logger.debug('Cargando tarjetas desde API');
    this.cargando = true;
    this.error = null;
    
    this.tarjetasService.getTarjetas().subscribe({
      next: (tarjetas) => {
        this.tarjetas = tarjetas;
        this.cargando = false;
        this.logger.success(`${tarjetas.length} tarjetas cargadas`);
      },
      error: (err) => {
        this.error = 'Error al cargar las tarjetas';
        this.cargando = false;
        this.logger.error('Error al cargar tarjetas', err);
      }
    });
  }

  get saldoTotal(): number {
    return this.tarjetas.reduce((sum, t) => {
      // Calcular saldo usado (invertir el signo si es negativo)
      return sum + (t.limiteCredito ? t.limiteCredito : 0);
    }, 0);
  }

  get limiteTotal(): number {
    return this.tarjetas.reduce((sum, t) => sum + (t.limiteCredito || 0), 0);
  }

  get disponibleTotal(): number {
    // Este cálculo requeriría información de transacciones
    // Por ahora retornamos el límite total
    return this.limiteTotal;
  }

  getPorcentajeUso(tarjeta: Tarjeta): number {
    if (!tarjeta.limiteCredito) return 0;
    // Este cálculo requiere información de transacciones
    // Por ahora retornamos 0
    return 0;
  }
}
