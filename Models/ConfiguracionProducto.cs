using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class ConfiguracionProducto
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El ID del ítem de producto es requerido.")]
		public int ItemProductoId { get; set; } // Relación con el ítem del producto

		[Required(ErrorMessage = "El ID de opción de característica de producto es requerido.")]
		public int OpcionCaracteristicaProductoId { get; set; } // Relación con la opción de característica del producto

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
