using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class EstadoOrdenCompra
	{
		[Key]
		public int Id { get; set; } // Id del estado de la orden

		[Required(ErrorMessage = "El estado es requerido.")]
		[StringLength(64, MinimumLength = 2, ErrorMessage = "El estado debe estar entre 2 y 64 caracteres.")]
		public string Estado { get; set; } // Estado de la orden

		public DateTime FechaCreacion { get; set; } = DateTime.Now; // Fecha de creación
		public DateTime FechaModificacion { get; set; } = DateTime.Now; // Fecha de modificación
	}
}
