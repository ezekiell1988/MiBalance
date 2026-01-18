import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelComponent } from '../../components/panel/panel.component';
import { LoggerService } from '../../service';

@Component({
  selector: 'app-tarjetas',
  standalone: true,
  imports: [CommonModule, PanelComponent],
  templateUrl: './tarjetas.page.html',
  styleUrls: ['./tarjetas.page.scss']
})
export class TarjetasPage implements OnInit {
  private readonly logger = inject(LoggerService).getLogger('TarjetasPage');

  tarjetas: any[] = [];

  ngOnInit() {
    this.logger.info('Página de tarjetas inicializada');
    this.cargarTarjetas();
  }

  cargarTarjetas() {
    // TODO: Conectar con API
    this.tarjetas = [
      {
        id: 1,
        nombre: 'Visa Clásica',
        numero: '****1234',
        tipo: 'Crédito',
        saldo: -50000,
        limite: 200000,
        fechaCorte: new Date('2026-01-25'),
        banco: 'BAC San José'
      },
      {
        id: 2,
        nombre: 'Mastercard Gold',
        numero: '****5678',
        tipo: 'Crédito',
        saldo: -35000,
        limite: 150000,
        fechaCorte: new Date('2026-01-20'),
        banco: 'Banco Nacional'
      }
    ];
  }

  get saldoTotal(): number {
    return this.tarjetas.reduce((sum, t) => sum + Math.abs(t.saldo), 0);
  }

  get limiteTotal(): number {
    return this.tarjetas.reduce((sum, t) => sum + t.limite, 0);
  }

  get disponibleTotal(): number {
    return this.tarjetas.reduce((sum, t) => sum + (t.limite - Math.abs(t.saldo)), 0);
  }

  getPorcentajeUso(tarjeta: any): number {
    return (Math.abs(tarjeta.saldo) / tarjeta.limite) * 100;
  }
}
