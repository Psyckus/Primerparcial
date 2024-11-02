using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class CategoriaPromocion
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El ID de la categoría es requerido.")]
		public int CategoriaProductoId { get; set; }  // Relación con la categoría del producto

		[Required(ErrorMessage = "El ID de la promoción es requerido.")]
		public int PromocionId { get; set; }  // Relación con la promoción

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
