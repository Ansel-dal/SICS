using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SICS.Server.Models;

public partial class SicsContext : DbContext
{
    public SicsContext()
    {
    }

    public SicsContext(DbContextOptions<SicsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Consumidor> Consumidors { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<EstadoPrestamo> EstadoPrestamos { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<NumeroCorrelativo> NumeroCorrelativos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosPedido> ProductosPedidos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=SICS;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10A53C6433");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Consumidor>(entity =>
        {
            entity.HasKey(e => e.IdConsumidor).HasName("PK__Consumid__9F510D960DD8D4F5");

            entity.ToTable("Consumidor");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.IdEntrega).HasName("PK__Entrega__C852F553D9A94F76");

            entity.ToTable("Entrega");

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.IdConsumidorNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdConsumidor)
                .HasConstraintName("FK__Entrega__IdConsu__7C4F7684");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdItem)
                .HasConstraintName("FK__Entrega__IdItem__7D439ABD");
        });

        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedido).HasName("PK__EstadoPe__86B983715995D137");

            entity.ToTable("EstadoPedido");

            entity.Property(e => e.IdEstadoPedido).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<EstadoPrestamo>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPrestamo).HasName("PK__EstadoPr__BCB87549ED1F708B");

            entity.ToTable("EstadoPrestamo");

            entity.Property(e => e.IdEstadoPrestamo).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.IdItem).HasName("PK__Items__51E84262F003E7B1");

            entity.Property(e => e.Codigo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Marca)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Items__IdCategor__286302EC");
        });

        modelBuilder.Entity<NumeroCorrelativo>(entity =>
        {
            entity.HasKey(e => e.IdNumeroCorrelativo).HasName("PK__NumeroCo__84369489E3613978");

            entity.ToTable("NumeroCorrelativo");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Prefijo)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__9D335DC3524E2DFE");

            entity.ToTable("Pedido");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdConsumidorNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdConsumidor)
                .HasConstraintName("FK__Pedido__IdConsum__5FB337D6");

            entity.HasOne(d => d.IdEstadoPedidoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdEstadoPedido)
                .HasConstraintName("FK__Pedido__IdEstado__75A278F5");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamo).HasName("PK__Prestamo__6FF194C0803FD106");

            entity.ToTable("Prestamo");

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.EstadoEntregado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoRecibido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaConfirmacionDevolucion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaDevolucion).HasColumnType("datetime");

            entity.HasOne(d => d.IdConsumidorNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdConsumidor)
                .HasConstraintName("FK__Prestamo__IdCons__5812160E");

            entity.HasOne(d => d.IdEstadoPrestamoNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdEstadoPrestamo)
                .HasConstraintName("FK__Prestamo__IdEsta__571DF1D5");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdItem)
                .HasConstraintName("FK__Prestamo__IdItem__59063A47");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__098892106036B143");

            entity.Property(e => e.Codigo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValueSql("((1))");
            entity.Property(e => e.FechaIngreso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdItem)
                .HasConstraintName("FK__Productos__IdIte__48CFD27E");
        });

        modelBuilder.Entity<ProductosPedido>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Producto__334B1F773E88F686");

            entity.ToTable("ProductosPedido");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.ProductosPedidos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__Productos__IdPed__6FE99F9F");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductosPedidos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Productos__IdPro__70DDC3D8");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6A8B3E4B7");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreApellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreApellidos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
