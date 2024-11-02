using Microsoft.EntityFrameworkCore;
using Parcial_1.Models;

namespace Parcial_1.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		// DbSets para las entidades
		public DbSet<Cliente> Clientes { get; set; }
		public DbSet<EstadoOrdenCompra> EstadosOrdenCompra { get; set; }
		public DbSet<MetodoEnvio> MetodosEnvio { get; set; }
		public DbSet<ItemOrdenCompra> ItemsOrdenCompra { get; set; }
		public DbSet<OrdenCompra> OrdenesCompra { get; set; }
		public DbSet<ItemCarritoCompras> ItemsCarritoCompras { get; set; }
		public DbSet<CarritoCompras> CarritosCompras { get; set; }
		public DbSet<Promocion> Promociones { get; set; }
		public DbSet<CategoriaPromocion> CategoriasPromocion { get; set; }
		public DbSet<CategoriaProducto> CategoriasProducto { get; set; }
		public DbSet<Producto> Productos { get; set; }
		public DbSet<ItemProducto> ItemsProducto { get; set; }
		public DbSet<CaracteristicaProducto> CaracteristicasProducto { get; set; }
		public DbSet<OpcionCaracteristicaProducto> OpcionesCaracteristicaProducto { get; set; }
		public DbSet<ConfiguracionProducto> ConfiguracionesProducto { get; set; }
		public DbSet<MetodoPagoCliente> MetodoPagoCliente { get; set; }
		public DbSet<TipoPago> TiposPago { get; set; }
		public DbSet<ResenaCliente> ResenaCliente { get; set; }
		public DbSet<Direccion> Direcciones { get; set; }
		public DbSet<Pais> Paises { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
