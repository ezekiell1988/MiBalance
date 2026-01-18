/**
 * Modelos para el sistema MiBalance basados en la API real
 */

// ============= Enumeraciones =============

export enum TipoTarjeta {
  Credito = 1,
  Debito = 2
}

export enum TipoTransaccion {
  Ingreso = 1,
  Gasto = 2,
  Transferencia = 3
}

export enum EstadoTransaccion {
  Pendiente = 1,
  Completada = 2,
  Cancelada = 3
}

export enum OrigenDatos {
  Manual = 1,
  FacturaIA = 2,
  VoucherIA = 3,
  EstadoBancarioIA = 4
}

export enum TipoCategoria {
  GastoFijo = 1,
  GastoVariable = 2,
  Ingreso = 3
}

// ============= Entidades Base =============

export interface BaseEntity {
  id: number;
  createdAt: Date | string;
  updatedAt?: Date | string;
  isActive: boolean;
}

// ============= Tarjetas =============

export interface Tarjeta extends BaseEntity {
  nombre: string;
  ultimosDigitos: string;
  tipo: TipoTarjeta;
  banco: string;
  limiteCredito?: number;
  diaCorte: number;
  diaPago: number;
  titular?: string;
}

export interface ResumenTarjeta {
  tarjeta: Tarjeta;
  totalGastado: number;
  cantidadTransacciones: number;
  limiteDisponible: number;
}

export interface ConsolidadoMensual {
  año: number;
  mes: number;
  tarjetas: {
    tarjeta: Tarjeta;
    totalGastado: number;
    saldoAnterior: number;
    pagoMinimo: number;
    fechaCorte: Date | string;
    fechaPago: Date | string;
  }[];
}

// ============= Categorías =============

export interface Categoria extends BaseEntity {
  nombre: string;
  descripcion?: string;
  tipo: TipoCategoria;
  color?: string;
  icono?: string;
  categoriaPadreId?: number;
}

// ============= Transacciones =============

export interface Transaccion extends BaseEntity {
  fecha: Date | string;
  tipo: TipoTransaccion;
  monto: number;
  descripcion: string;
  categoriaId: number;
  tarjetaId?: number;
  proveedorId?: number;
  clienteId?: number;
  numeroDocumento?: string;
  estado: EstadoTransaccion;
  origen: OrigenDatos;
  notasIA?: string;
  // Relaciones opcionales (cuando se incluyen en la respuesta)
  categoria?: Categoria;
  tarjeta?: Tarjeta;
}

export interface TransaccionFiltros {
  fechaDesde?: Date | string;
  fechaHasta?: Date | string;
  tipo?: TipoTransaccion;
  categoriaId?: number;
}

export interface TransaccionCreate {
  fecha: Date | string;
  tipo: TipoTransaccion;
  monto: number;
  descripcion: string;
  categoriaId: number;
  tarjetaId?: number;
  proveedorId?: number;
  clienteId?: number;
  numeroDocumento?: string;
  estado?: EstadoTransaccion;
  origen?: OrigenDatos;
  notasIA?: string;
}

export interface TransaccionUpdate extends TransaccionCreate {
  id: number;
}

// ============= Reportes =============

export interface EstadoResultados {
  periodo: {
    fechaDesde: Date | string;
    fechaHasta: Date | string;
  };
  totalIngresos: number;
  totalGastos: number;
  resultadoNeto: number;
}

export interface FlujoCajaMensual {
  mes: number;
  nombreMes: string;
  ingresos: number;
  gastos: number;
  saldo: number;
}

export interface FlujoCaja {
  año: number;
  meses: FlujoCajaMensual[];
}

export interface BalanceGeneral {
  fecha: Date | string;
  activos: {
    cuentasPorCobrar: number;
    efectivo?: number;
    total: number;
  };
  pasivos: {
    cuentasPorPagar: number;
    total: number;
  };
  patrimonio: number;
}

// ============= Documentos IA =============

export interface ResultadoProcesamientoIA {
  exitoso: boolean;
  mensajeError?: string;
  datosExtraidos?: {
    fecha?: Date | string;
    monto?: number;
    descripcion?: string;
    proveedor?: string;
    numeroDocumento?: string;
    [key: string]: any;
  };
  confianza?: number;
}

// ============= Configuración =============

export interface ConfiguracionResponse {
  nameCompany: string;
  sloganCompany: string;
  apiVersion: string;
  features: {
    enableAzureAI: boolean;
    enableNotifications: boolean;
    enableReports: boolean;
  };
  settings: {
    maxUploadSize: number;
    sessionTimeout: number;
  };
}

// ============= Respuestas API =============

export interface ApiResponse<T> {
  success: boolean;
  data?: T;
  error?: string;
  message?: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  totalCount: number;
  pageSize: number;
  currentPage: number;
  totalPages: number;
}
