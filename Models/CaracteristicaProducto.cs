using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class CaracteristicaProducto
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El ID de categoría del producto es requerido.")]
		public int CategoriaProductoId { get; set; } // Relación con la categoría de producto

		[Required(ErrorMessage = "El nombre es requerido.")]
		[StringLength(255, MinimumLength = 5, ErrorMessage = "El nombre debe tener entre 5 y 255 caracteres.")]
		public string Nombre { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;

	}
}
