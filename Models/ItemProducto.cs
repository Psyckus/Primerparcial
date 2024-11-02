using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class ItemProducto
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El ID del producto es requerido.")]
		public int ProductoId { get; set; }  // Relación con el producto

		[Required(ErrorMessage = "El código de barras es requerido.")]
		[StringLength(255, MinimumLength = 5, ErrorMessage = "El código de barras debe tener entre 5 y 255 caracteres.")]
		public string CodigoBarras { get; set; }

		[Range(0, 9999, ErrorMessage = "La cantidad disponible debe estar entre 0 y 9,999.")]
		public int CantidadDisponible { get; set; }

		[Required(ErrorMessage = "La URL de la imagen del producto es requerida.")]
		[StringLength(1024, MinimumLength = 5, ErrorMessage = "La URL de la imagen debe tener entre 5 y 1024 caracteres.")]
		public string UrlImagen { get; set; }

		[Range(1, 999999, ErrorMessage = "El precio debe estar entre 1 y 999,999.")]
		public decimal Precio { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
