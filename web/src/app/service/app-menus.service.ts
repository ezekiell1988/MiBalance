import { Injectable } from "@angular/core";

/**
 * Interfaz para items del menú
 */
export interface MenuItem {
  icon?: string;
  iconMobile?: string;
  title: string;
  url: string;
  caret?: string;
  submenu?: MenuItem[];
}

/**
 * Servicio para gestionar el menú de la aplicación
 * Siguiendo buenas prácticas de Angular 20+
 */
@Injectable({
  providedIn: "root",
})
export class AppMenuService {
  /**
   * Configuración del menú centralizada
   * Facilita mantenimiento y testing
   */
  private readonly menuConfig: MenuItem[] = [
    {
      icon: "fa fa-home",
      iconMobile: "home-outline",
      title: "Dashboard",
      url: "/dashboard",
    },
    {
      icon: "fa fa-exchange-alt",
      iconMobile: "swap-horizontal-outline",
      title: "Transacciones",
      url: "/transacciones",
    },
    {
      icon: "fa fa-credit-card",
      iconMobile: "card-outline",
      title: "Tarjetas",
      url: "/tarjetas",
    },
    {
      icon: "fa fa-tags",
      iconMobile: "pricetags-outline",
      title: "Categorías",
      url: "/categorias",
    },
    {
      icon: "fa fa-chart-bar",
      iconMobile: "bar-chart-outline",
      title: "Reportes",
      url: "/reportes",
      caret: "true",
      submenu: [
        {
          url: "/reportes",
          title: "Estado de Resultados",
          icon: "fa fa-chart-line",
          iconMobile: "trending-up-outline",
        },
        {
          url: "/reportes",
          title: "Flujo de Caja",
          icon: "fa fa-dollar-sign",
          iconMobile: "cash-outline",
        },
        {
          url: "/reportes",
          title: "Balance General",
          icon: "fa fa-balance-scale",
          iconMobile: "scale-outline",
        },
      ],
    },
  ];

  /**
   * Retorna una copia profunda del menú para evitar mutaciones
   */
  getAppMenus(): MenuItem[] {
    // Retorna deep copy para prevenir mutaciones accidentales
    return JSON.parse(JSON.stringify(this.menuConfig));
  }

  /**
   * Busca un item del menú por URL
   */
  findMenuItemByUrl(url: string): MenuItem | null {
    const search = (items: MenuItem[]): MenuItem | null => {
      for (const item of items) {
        if (item.url === url) return item;
        if (item.submenu) {
          const found = search(item.submenu);
          if (found) return found;
        }
      }
      return null;
    };
    return search(this.menuConfig);
  }

  /**
   * Obtiene todos los items del menú de forma plana
   */
  getFlatMenuItems(): MenuItem[] {
    const flatten = (items: MenuItem[]): MenuItem[] => {
      return items.reduce((acc, item) => {
        acc.push(item);
        if (item.submenu) {
          acc.push(...flatten(item.submenu));
        }
        return acc;
      }, [] as MenuItem[]);
    };
    return flatten(this.menuConfig);
  }
}
