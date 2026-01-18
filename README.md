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

- .NET 10 SDK
- Docker
- SQL Server en Docker
- Cuenta Azure con AI Foundry configurado

## 🔧 Configuración

1. Clonar el repositorio
2. Configurar credenciales en `appsettings.json`
3. Ejecutar migraciones de EF Core
4. Iniciar la aplicación

## 📝 Base de Datos

El sistema utiliza un modelo contable estándar con:
- Catálogo de cuentas contables
- Asientos contables (debe/haber)
- Categorías de gastos e ingresos
- Clientes y proveedores
- Tarjetas de crédito y débito
- Transacciones bancarias

## 🤝 Contribuciones

Proyecto personal para gestión financiera familiar.

## 📄 Licencia

Privado - Uso personal
