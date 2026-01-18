import { Routes } from "@angular/router";
import {
  HomePage,
  ErrorPage,
  LoginPage,
  DashboardPage,
  TransaccionesPage,
  TarjetasPage,
  ReportesPage,
  CategoriasPage
} from "./pages";
import { AuthGuard } from "./shared/guards";

export const routes: Routes = [
  {
    path: "",
    redirectTo: "/dashboard",
    pathMatch: "full",
  },
  {
    path: "login",
    component: LoginPage,
    data: { title: "Iniciar Sesión" },
  },
  {
    path: "home",
    component: HomePage,
    data: { title: "Inicio" },
    // canActivate: [AuthGuard],
  },
  {
    path: "dashboard",
    component: DashboardPage,
    data: { title: "Dashboard" },
    // canActivate: [AuthGuard],
  },
  {
    path: "transacciones",
    component: TransaccionesPage,
    data: { title: "Transacciones" },
    // canActivate: [AuthGuard],
  },
  {
    path: "tarjetas",
    component: TarjetasPage,
    data: { title: "Tarjetas" },
    // canActivate: [AuthGuard],
  },
  {
    path: "categorias",
    component: CategoriasPage,
    data: { title: "Categorías" },
    // canActivate: [AuthGuard],
  },
  {
    path: "reportes",
    component: ReportesPage,
    data: { title: "Reportes" },
    // canActivate: [AuthGuard],
  },
  {
    path: "**",
    component: ErrorPage,
    data: { title: "404 Error" },
  },
];
