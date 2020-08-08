using App.ContextoPrimusDb.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.ContextoPrimusDb
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        public DbSet<Productos> Productos { get; set; }

        public DbSet<Bodegas> Bodegas { get; set; }

        public DbSet<Categorias> Categorias { get; set; }

        public DbSet<Devoluciones> Devoluciones { get; set; }

        public DbSet<Gastos> Gastos { get; set; }

        public DbSet<Proveedores> Proveedores { get; set; }

        public DbSet<Marcas> Marcas { get; set; }

        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Ventas> Ventas { get; set; }

        public DbSet<DevolucionxProducto> DevolucionxProducto { get; set; }

        public DbSet<GastosxProductos> GastosxProductos { get; set; }

        public DbSet<ProveedoresxProducto> ProveedoresxProducto { get; set; }

        public DbSet<VentasxProductos> VentasxProductos { get; set; }

    }
}
