using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class Promocion
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre corto es requerido.")]
		[StringLength(64, MinimumLength = 5, ErrorMessage = "El nombre corto debe tener entre 5 y 64 caracteres.")]
		public string NombreCorto { get; set; }

		[Required(ErrorMessage = "La descripción es requerida.")]
		[StringLength(132, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 132 caracteres.")]
		public string Descripcion { get; set; }

		[Required(ErrorMessage = "El porcentaje de descuento es requerido.")]
		[Range(0, 90, ErrorMessage = "El porcentaje de descuento debe estar entre 0 y 90.")]
		public int PorcentajeDescuento { get; set; }

		[Required(ErrorMessage = "La fecha de inicio es requerida.")]
		public DateTime FechaInicia { get; set; }

		[Required(ErrorMessage = "La fecha de finalización es requerida.")]
		public DateTime FechaFinaliza { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
