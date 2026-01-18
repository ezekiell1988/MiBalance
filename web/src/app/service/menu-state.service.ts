import { Injectable, EventEmitter } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

/**
 * Servicio para gestionar el estado del menú
 */
@Injectable({
  providedIn: 'root'
})
export class MenuStateService {
  private _menuExpanded = new BehaviorSubject<boolean>(false);
  public menuExpanded$ = this._menuExpanded.asObservable();
  
  private _sidebarMenuState = new BehaviorSubject<boolean>(false);

  toggleMenu(): void {
    this._menuExpanded.next(!this._menuExpanded.value);
  }

  expandMenu(): void {
    this._menuExpanded.next(true);
  }

  collapseMenu(): void {
    this._menuExpanded.next(false);
  }

  isMenuExpanded(): boolean {
    return this._menuExpanded.value;
  }

  setSidebarMenuState(state: boolean): void {
    this._sidebarMenuState.next(state);
  }

  getSidebarMenuState() {
    return this._sidebarMenuState.asObservable();
  }
}
