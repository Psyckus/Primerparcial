using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class Direccion
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "La provincia es requerida.")]
		[Range(1, 7, ErrorMessage = "La provincia debe estar en el rango de 1 a 7.")]
		public int ProvinciaId { get; set; }

		[Required(ErrorMessage = "El cantón es requerido.")]
		public int CantonId { get; set; }

		[Required(ErrorMessage = "El distrito es requerido.")]
		public int DistritoId { get; set; }

		[Required(ErrorMessage = "La dirección exacta es requerida.")]
		[StringLength(500, ErrorMessage = "La dirección exacta no debe exceder los 500 caracteres.")]
		public string DireccionExacta { get; set; }

		[Required(ErrorMessage = "El código postal es requerido.")]
		[StringLength(10, ErrorMessage = "El código postal no debe exceder los 10 caracteres.")]
		public string CodigoPostal { get; set; }

		[Required(ErrorMessage = "El país es requerido.")]
		public int PaisId { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
