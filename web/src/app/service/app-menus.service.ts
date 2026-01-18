import { Injectable } from '@angular/core';

export interface MenuItem {
  text?: string;
  icon?: string;
  path?: string;
  badge?: string;
  badgeClass?: string;
  children?: MenuItem[];
  target?: string;
  caret?: boolean;
  highlight?: boolean;
  img?: string;
  label?: string;
  title?: boolean;
}

/**
 * Servicio simplificado para gestionar el menú de la aplicación
 */
@Injectable({
  providedIn: 'root'
})
export class AppMenuService {
  private appMenus: MenuItem[] = [
    {
      icon: 'fa fa-th',
      text: 'Dashboard',
      path: '/dashboard'
    },
    {
      icon: 'fa fa-credit-card',
      text: 'Tarjetas',
      path: '/tarjetas'
    },
    {
      icon: 'fa fa-exchange-alt',
      text: 'Transacciones',
      path: '/transacciones'
    },
    {
      icon: 'fa fa-tags',
      text: 'Categorías',
      path: '/categorias'
    },
    {
      icon: 'fa fa-chart-line',
      text: 'Reportes',
      path: '/reportes'
    }
  ];

  getAppMenus(): MenuItem[] {
    return this.appMenus;
  }
}
