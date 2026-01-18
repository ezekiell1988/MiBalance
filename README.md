# MiBalance - Sistema de Control Financiero Personal

Sistema de gestión financiera personal desarrollado con .NET 10, Entity Framework Core y .NET MAUI, integrado con Azure AI Foundry para extracción automática de datos de documentos financieros.

## 🎯 Características

- **Gestión de Cuentas por Pagar**: Control de tarjetas de crédito y obligaciones
- **Cuentas por Cobrar**: Seguimiento de préstamos a amigos y familiares
- **Gastos Fijos y Variables**: Clasificación automática de gastos
- **Extracción Automática de Datos**: Procesamiento de facturas, vouchers y estados bancarios con Azure AI Foundry
- **Reportes Financieros**: Balance general, estado de resultados, flujo de caja
- **Recordatorios**: Alertas para pagos y cobros pendientes

## 🏗️ Arquitectura

```
MiBalance/
├── src/
│   ├── MiBalance.Api/          # API REST con Swagger
│   ├── MiBalance.Core/         # Entidades y lógica de negocio
│   ├── MiBalance.Infrastructure/ # EF Core, Azure AI, repositorios
│   └── MiBalance.Maui/         # Aplicación móvil/desktop
└── docs/                        # Documentación
```

## 🚀 Tecnologías

- .NET 10
- Entity Framework Core
- SQL Server (Docker)
- Azure AI Foundry (GPT-5.2)
- Swagger/OpenAPI
- .NET MAUI
- Redis (caché)

## 📦 Requisitos Previos

- .NET 10 SDK ([Descargar](https://dotnet.microsoft.com/download))
- Docker (opcional, para SQL Server)
- SQL Server (local o Docker)
- Cuenta Azure con AI Foundry configurado (opcional)

## 🚀 Inicio Rápido

### Primera Vez
```bash
# Clonar el repositorio
git clone <repo-url>
cd MiBalance

# Ejecutar configuración inicial
./scripts/setup.sh
```

### Desarrollo
```bash
# Iniciar servidor en modo desarrollo (auto-reload)
./scripts/start-dev.sh

# El servidor estará disponible en:
# - HTTP: http://localhost:5000
# - Swagger: http://localhost:5000/swagger
```

### Producción
```bash
# Iniciar servidor en modo producción
./scripts/start.sh
```

### Detener Servidor
```bash
# Detener todos los procesos
./scripts/stop.sh

# O presionar Ctrl+C en la terminal del servidor
```

## 🔧 Configuración Detallada

### 1. Base de Datos

Edita `src/MiBalance.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=172.191.128.24,1433;Database=mibalancedb;User Id=budgetuser;Password=YourPassword123!;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

### 2. Azure AI (Opcional)

Para procesamiento de documentos con IA:

```json
{
  "AzureAI": {
    "Endpoint": "https://your-resource.cognitiveservices.azure.com/",
    "ApiKey": "your-api-key-here"
  }
}
```

### 3. Migraciones de EF Core

```bash
# Las migraciones se aplican automáticamente al iniciar el servidor
# O manualmente con:
export PATH="$PATH:$HOME/.dotnet/tools"

dotnet ef database update \
  --project src/MiBalance.Infrastructure/MiBalance.Infrastructure.csproj \
  --startup-project src/MiBalance.Api/MiBalance.Api.csproj
```

## 📚 Scripts Disponibles

| Script | Descripción |
|--------|-------------|
| `./scripts/setup.sh` | Configuración inicial del proyecto |
| `./scripts/start.sh` | Iniciar en modo producción |
| `./scripts/start-dev.sh` | Iniciar en modo desarrollo (auto-reload) |
| `./scripts/stop.sh` | Detener todos los procesos |
| `./scripts/reset-db.sh` | Reiniciar base de datos (⚠️ elimina datos) |

Ver documentación completa en [scripts/README.md](scripts/README.md)

## 📝 Base de Datos

El sistema utiliza un modelo contable estándar con:
- Catálogo de cuentas contables (18 cuentas del plan contable)
- Asientos contables (debe/haber)
- Categorías de gastos e ingresos (5 categorías seed)
- Clientes y proveedores
- Tarjetas de crédito y débito
- Transacciones bancarias

### Tablas Principales
- `Categorias` - Categorías de transacciones
- `CuentasContables` - Plan contable
- `Tarjetas` - Tarjetas de crédito/débito
- `Transacciones` - Movimientos financieros
- `AsientosContables` - Registros contables
- `CuentasPorCobrar` / `CuentasPorPagar` - Cuentas por cobrar/pagar
- `Clientes` / `Proveedores` - Directorio de contactos
- `Recordatorios` - Alertas de pagos/cobros

## 📖 Documentación

- [Guía de Inicio Rápido](INICIO_RAPIDO.md) - Inicio rápido y comandos esenciales
- [Scripts README](scripts/README.md) - Documentación de scripts de automatización
- [Comandos de Despliegue](docs/COMANDOS_DESPLIEGUE.md) - Historial detallado de comandos ejecutados

## 🔌 API Endpoints

Una vez iniciado el servidor, visita:
- **Swagger UI**: http://localhost:5000/swagger

### Principales Endpoints

#### Transacciones
- `GET /api/transacciones` - Listar transacciones
- `POST /api/transacciones` - Crear transacción
- `GET /api/transacciones/resumen-por-categoria` - Resumen por categoría

#### Tarjetas
- `GET /api/tarjetas` - Listar tarjetas
- `POST /api/tarjetas` - Crear tarjeta
- `GET /api/tarjetas/{id}/resumen` - Resumen de tarjeta
- `GET /api/tarjetas/consolidado-mensual` - Consolidado mensual

#### Documentos (Azure AI)
- `POST /api/documentos/procesar-factura` - Procesar factura con IA
- `POST /api/documentos/procesar-voucher` - Procesar voucher con IA
- `POST /api/documentos/procesar-estado-bancario` - Procesar estado bancario

#### Reportes
- `GET /api/reportes/estado-resultados` - Estado de resultados
- `GET /api/reportes/flujo-caja` - Flujo de caja
- `GET /api/reportes/balance-general` - Balance general
- `GET /api/reportes/tendencias` - Análisis de tendencias

## 🔍 Estado del Proyecto

✅ **Funcional y Listo para Desarrollo**

- ✅ Backend API compilado y ejecutándose
- ✅ Base de datos configurada con datos seed
- ✅ Migraciones aplicadas correctamente
- ✅ Scripts de automatización creados
- ✅ Swagger UI disponible
- ✅ Servidor corriendo en http://localhost:5000
- ⚠️ MAUI app requiere configuración adicional

**Última actualización**: 2026-01-18  
**Versión .NET**: 10.0.102  
**Versión EF Core**: 10.0.0

## 🤝 Contribuciones

Proyecto personal para gestión financiera familiar.

## 📄 Licencia

Privado - Uso personal
