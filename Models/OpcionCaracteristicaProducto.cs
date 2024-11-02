using System;
using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class OpcionCaracteristicaProducto
	{
		[Key]
		public int IdOpcionCaracteristicaProducto { get; set; }

		public int IdCaracteristicaProducto { get; set; }

		[Required]
		public string Valor { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
