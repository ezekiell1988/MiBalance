using Microsoft.EntityFrameworkCore;
using MiBalance.Core.Entities;

namespace MiBalance.Infrastructure.Data;

public class MiBalanceDbContext : DbContext
{
    public MiBalanceDbContext(DbContextOptions<MiBalanceDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<CuentaContable> CuentasContables { get; set; }
    public DbSet<AsientoContable> AsientosContables { get; set; }
    public DbSet<DetalleAsiento> DetallesAsientos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarjeta> Tarjetas { get; set; }
    public DbSet<Transaccion> Transacciones { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<CuentaPorCobrar> CuentasPorCobrar { get; set; }
    public DbSet<CuentaPorPagar> CuentasPorPagar { get; set; }
    public DbSet<PagoCuenta> PagosCuentas { get; set; }
    public DbSet<Recordatorio> Recordatorios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de CuentaContable
        modelBuilder.Entity<CuentaContable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Codigo).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => e.Codigo).IsUnique();
            
            entity.HasOne(e => e.CuentaPadre)
                .WithMany(e => e.SubCuentas)
                .HasForeignKey(e => e.CuentaPadreId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de AsientoContable
        modelBuilder.Entity<AsientoContable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Numero).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Concepto).IsRequired().HasMaxLength(500);
            entity.HasIndex(e => e.Numero).IsUnique();
            
            entity.HasOne(e => e.Transaccion)
                .WithOne(t => t.AsientoContable)
                .HasForeignKey<AsientoContable>(e => e.TransaccionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de DetalleAsiento
        modelBuilder.Entity<DetalleAsiento>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Debe).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Haber).HasColumnType("decimal(18,2)");
            
            entity.HasOne(e => e.AsientoContable)
                .WithMany(a => a.Detalles)
                .HasForeignKey(e => e.AsientoContableId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(e => e.CuentaContable)
                .WithMany(c => c.DetallesAsientos)
                .HasForeignKey(e => e.CuentaContableId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de Categoria
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            
            entity.HasOne(e => e.CategoriaPadre)
                .WithMany(e => e.SubCategorias)
                .HasForeignKey(e => e.CategoriaPadreId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de Tarjeta
        modelBuilder.Entity<Tarjeta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.UltimosDigitos).IsRequired().HasMaxLength(4);
            entity.Property(e => e.Banco).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LimiteCredito).HasColumnType("decimal(18,2)");
        });

        // Configuración de Transaccion
        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Descripcion).IsRequired().HasMaxLength(500);
            
            entity.HasOne(e => e.Categoria)
                .WithMany(c => c.Transacciones)
                .HasForeignKey(e => e.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Tarjeta)
                .WithMany(t => t.Transacciones)
                .HasForeignKey(e => e.TarjetaId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Proveedor)
                .WithMany(p => p.Transacciones)
                .HasForeignKey(e => e.ProveedorId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Transacciones)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de Proveedor
        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
            entity.Property(e => e.RUC).HasMaxLength(20);
        });

        // Configuración de Cliente
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Identificacion).HasMaxLength(20);
        });

        // Configuración de CuentaPorCobrar
        modelBuilder.Entity<CuentaPorCobrar>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.MontoOriginal).HasColumnType("decimal(18,2)");
            entity.Property(e => e.MontoPendiente).HasColumnType("decimal(18,2)");
            entity.Property(e => e.TasaInteres).HasColumnType("decimal(5,2)");
            
            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.CuentasPorCobrar)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de CuentaPorPagar
        modelBuilder.Entity<CuentaPorPagar>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.MontoOriginal).HasColumnType("decimal(18,2)");
            entity.Property(e => e.MontoPendiente).HasColumnType("decimal(18,2)");
            entity.Property(e => e.InteresesMora).HasColumnType("decimal(18,2)");
            
            entity.HasOne(e => e.Tarjeta)
                .WithMany()
                .HasForeignKey(e => e.TarjetaId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.Proveedor)
                .WithMany()
                .HasForeignKey(e => e.ProveedorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de PagoCuenta
        modelBuilder.Entity<PagoCuenta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
            
            entity.HasOne(e => e.CuentaPorCobrar)
                .WithMany(c => c.Pagos)
                .HasForeignKey(e => e.CuentaPorCobrарId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(e => e.CuentaPorPagar)
                .WithMany(c => c.Pagos)
                .HasForeignKey(e => e.CuentaPorPagarId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuración de Recordatorio
        modelBuilder.Entity<Recordatorio>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            
            entity.HasOne(e => e.Transaccion)
                .WithMany(t => t.Recordatorios)
                .HasForeignKey(e => e.TransaccionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Datos semilla - Cuentas contables básicas
        SeedCuentasContables(modelBuilder);
    }

    private void SeedCuentasContables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CuentaContable>().HasData(
            // Activos
            new CuentaContable { Id = 1, Codigo = "1", Nombre = "ACTIVO", Tipo = TipoCuenta.Activo, Naturaleza = NaturalezaCuenta.Deudora, Nivel = 1, EsDeMovimiento = false },
            new CuentaContable { Id = 2, Codigo = "1.1", Nombre = "ACTIVO CORRIENTE", Tipo = TipoCuenta.Activo, Naturaleza = NaturalezaCuenta.Deudora, Nivel = 2, EsDeMovimiento = false, CuentaPadreId = 1 },
            new CuentaContable { Id = 3, Codigo = "1.1.1", Nombre = "Efectivo y Equivalentes", Tipo = TipoCuenta.Activo, Naturaleza = NaturalezaCuenta.Deudora, Nivel = 3, EsDeMovimiento = true, CuentaPadreId = 2 },
            new CuentaContable { Id = 4, Codigo = "1.1.2", Nombre = "Cuentas por Cobrar", Tipo = TipoCuenta.Activo, Naturaleza = NaturalezaCuenta.Deudora, Nivel = 3, EsDeMovimiento = true, CuentaPadreId = 2 },
            
            // Pasivos
            new CuentaContable { Id = 5, Codigo = "2", Nombre = "PASIVO", Tipo = TipoCuenta.Pasivo, Naturaleza = NaturalezaCuenta.Acreedora, Nivel = 1, EsDeMovimiento = false },
            new CuentaContable { Id = 6, Codigo = "2.1", Nombre = "PASIVO CORRIENTE", Tipo = TipoCuenta.Pasivo, Naturaleza = NaturalezaCuenta.Acreedora, Nivel = 2, EsDeMovimiento = false, CuentaPadreId = 5 },
            new CuentaContable { Id = 7, Codigo = "2.1.1", Nombre = "Cuentas por Pagar", Tipo = TipoCuenta.Pasivo, Naturaleza = NaturalezaCuenta.Acreedora, Nivel = 3, EsDeMovimiento = true, CuentaPadreId = 6 },
            new CuentaContable { Id = 8, Codigo = "2.1.2", Nombre = "Tarjetas de Crédito", Tipo = TipoCuenta.Pasivo, Naturaleza = NaturalezaCuenta.Acreedora, Nivel = 3, EsDeMovimiento = true, CuentaPadreId = 6 },
            
            // Patrimonio
            new CuentaContable { Id = 9, Codigo = "3", Nombre = "PATRIMONIO", Tipo = TipoCuenta.Patrimonio, Naturaleza = NaturalezaCuenta.Acreedora, Nivel = 1, EsDeMovimiento = false },
            new CuentaContable { Id = 10, Codigo = "3.1", Nombre = "Capital", Tipo = TipoCuenta.Patrimonio, Naturaleza = NaturalezaCuenta.Acreedora, Nivel = 2, EsDeMovimiento = true, CuentaPadreId = 9 },
            
            // Ingresos
            new CuentaContable { Id = 11, Codigo = "4", Nombre = "INGRESOS", Tipo = TipoCuenta.Ingreso, Naturaleza = NaturalezaCuenta.Acreedora, Nivel = 1, EsDeMovimiento = false },
            new CuentaContable { Id = 12, Codigo = "4.1", Nombre = "Ingresos Operacionales", Tipo = TipoCuenta.Ingreso, Naturaleza = NaturalezaCuenta.Acreedora, Nivel = 2, EsDeMovimiento = false, CuentaPadreId = 11 },
            new CuentaContable { Id = 13, Codigo = "4.1.1", Nombre = "Salarios", Tipo = TipoCuenta.Ingreso, Naturaleza = NaturalezaCuenta.Acreedora, Nivel = 3, EsDeMovimiento = true, CuentaPadreId = 12 },
            
            // Gastos
            new CuentaContable { Id = 14, Codigo = "5", Nombre = "GASTOS", Tipo = TipoCuenta.Gasto, Naturaleza = NaturalezaCuenta.Deudora, Nivel = 1, EsDeMovimiento = false },
            new CuentaContable { Id = 15, Codigo = "5.1", Nombre = "Gastos Fijos", Tipo = TipoCuenta.Gasto, Naturaleza = NaturalezaCuenta.Deudora, Nivel = 2, EsDeMovimiento = false, CuentaPadreId = 14 },
            new CuentaContable { Id = 16, Codigo = "5.1.1", Nombre = "Servicios Básicos", Tipo = TipoCuenta.Gasto, Naturaleza = NaturalezaCuenta.Deudora, Nivel = 3, EsDeMovimiento = true, CuentaPadreId = 15 },
            new CuentaContable { Id = 17, Codigo = "5.2", Nombre = "Gastos Variables", Tipo = TipoCuenta.Gasto, Naturaleza = NaturalezaCuenta.Deudora, Nivel = 2, EsDeMovimiento = false, CuentaPadreId = 14 },
            new CuentaContable { Id = 18, Codigo = "5.2.1", Nombre = "Alimentación", Tipo = TipoCuenta.Gasto, Naturaleza = NaturalezaCuenta.Deudora, Nivel = 3, EsDeMovimiento = true, CuentaPadreId = 17 }
        );

        // Categorías básicas
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nombre = "Servicios Básicos", Tipo = TipoCategoria.GastoFijo, Color = "#FF5733" },
            new Categoria { Id = 2, Nombre = "Alimentación", Tipo = TipoCategoria.GastoVariable, Color = "#33FF57" },
            new Categoria { Id = 3, Nombre = "Transporte", Tipo = TipoCategoria.GastoVariable, Color = "#3357FF" },
            new Categoria { Id = 4, Nombre = "Salario", Tipo = TipoCategoria.Ingreso, Color = "#FFD700" },
            new Categoria { Id = 5, Nombre = "Entretenimiento", Tipo = TipoCategoria.GastoVariable, Color = "#FF33F5" }
        );
    }
}
