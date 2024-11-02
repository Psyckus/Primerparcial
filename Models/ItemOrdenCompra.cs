using System;
using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class ItemOrdenCompra
	{
		[Key]
		public int IdItemOrdenCompra { get; set; }

		public int IdItemProducto { get; set; }
		public int IdOrdenCompra { get; set; }

		[Range(0, 999, ErrorMessage = "La cantidad debe estar entre 0 y 999.")]
		public int Cantidad { get; set; }

		[Range(0, 999999, ErrorMessage = "El precio debe estar entre 0 y 999,999.")]
		public decimal Precio { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
