import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { PanelComponent } from '../../components/panel/panel.component';
import { LoggerService } from '../../service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, PanelComponent],
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss']
})
export class DashboardPage implements OnInit {
  private readonly logger = inject(LoggerService).getLogger('DashboardPage');

  // Datos del dashboard
  resumenFinanciero = {
    ingresos: 0,
    gastos: 0,
    balance: 0,
    saldoTarjetas: 0
  };

  transaccionesRecientes: any[] = [];
  categoriasGastos: any[] = [];

  ngOnInit() {
    this.logger.info('Dashboard inicializado');
    this.cargarDatosDashboard();
  }

  cargarDatosDashboard() {
    this.logger.debug('Cargando datos del dashboard');
    
    // TODO: Conectar con el servicio real del backend
    // Por ahora usar datos de ejemplo
    this.resumenFinanciero = {
      ingresos: 5000,
      gastos: 3200,
      balance: 1800,
      saldoTarjetas: -1500
    };

    this.transaccionesRecientes = [
      { id: 1, descripcion: 'Supermercado', monto: -150, fecha: new Date(), categoria: 'Alimentación' },
      { id: 2, descripcion: 'Salario', monto: 5000, fecha: new Date(), categoria: 'Ingresos' },
      { id: 3, descripcion: 'Gasolina', monto: -80, fecha: new Date(), categoria: 'Transporte' }
    ];

    this.categoriasGastos = [
      { categoria: 'Alimentación', monto: 800, porcentaje: 25 },
      { categoria: 'Transporte', monto: 500, porcentaje: 15.6 },
      { categoria: 'Servicios', monto: 400, porcentaje: 12.5 },
      { categoria: 'Entretenimiento', monto: 300, porcentaje: 9.4 },
      { categoria: 'Otros', monto: 1200, porcentaje: 37.5 }
    ];
  }

  get balanceClass(): string {
    return this.resumenFinanciero.balance >= 0 ? 'text-success' : 'text-danger';
  }
}
