using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class MetodoEnvio
	{
		[Key]
		public int Id { get; set; } // Id del método de envío

		[Required(ErrorMessage = "El nombre es requerido.")]
		[StringLength(132, MinimumLength = 5, ErrorMessage = "El nombre debe estar entre 5 y 132 caracteres.")]
		public string Nombre { get; set; } // Nombre del método de envío

		[Range(0, 999999, ErrorMessage = "El precio debe estar entre 0 y 999,999.")]
		public decimal Precio { get; set; } // Precio del método de envío

		public DateTime FechaCreacion { get; set; } = DateTime.Now; // Fecha de creación
		public DateTime FechaModificacion { get; set; } = DateTime.Now; // Fecha de modificación
	}
}
