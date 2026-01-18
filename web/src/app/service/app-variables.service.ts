import { Injectable, EventEmitter } from '@angular/core';

/**
 * Servicio simplificado para variables de la aplicación
 */
@Injectable({
  providedIn: 'root'
})
export class AppVariablesService {
  public variablesReload = new EventEmitter<void>();

  private appVariables = {
    version: '1.0.0',
    font: {
      family: '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif',
      bodyFontSize: '14px',
      bodyFontWeight: '400',
      bodyLineHeight: '1.5'
    },
    color: {
      theme: '#348fe2',
      themeRgb: '52, 143, 226',
      primary: '#348fe2',
      primaryRgb: '52, 143, 226',
      success: '#00acac',
      successRgb: '0, 172, 172',
      warning: '#f59c1a',
      warningRgb: '245, 156, 26',
      danger: '#ff5b57',
      dangerRgb: '255, 91, 87',
      info: '#00acac',
      infoRgb: '0, 172, 172',
      dark: '#2d353c',
      darkRgb: '45, 53, 60',
      light: '#f2f3f4',
      lightRgb: '242, 243, 244',
      gray: {
        100: '#f8f9fa',
        200: '#e9ecef',
        300: '#dee2e6',
        400: '#ced4da',
        500: '#adb5bd',
        600: '#6c757d',
        700: '#495057',
        800: '#343a40',
        900: '#212529'
      }
    },
    border: {
      radius: '6px'
    }
  };

  getAppVariables() {
    return this.appVariables;
  }
}
