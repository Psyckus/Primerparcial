using System;
using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class OrdenCompra
	{
		[Key]
		public int IdOrdenCompra { get; set; }

		public int IdCliente { get; set; }
		public int IdMetodoPago { get; set; }
		public int IdDireccionEnvio { get; set; }
		public int IdMetodoEnvio { get; set; }

		[Range(0, 999999, ErrorMessage = "El monto de la orden debe estar entre 0 y 999,999.")]
		public decimal MontoOrden { get; set; }

		public int IdEstadoOrden { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
