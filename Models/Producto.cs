using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class Producto
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El ID de la categoría es requerido.")]
		public int CategoriaProductoId { get; set; }  // Relación con la categoría del producto

		[Required(ErrorMessage = "El nombre del producto es requerido.")]
		[StringLength(132, MinimumLength = 5, ErrorMessage = "El nombre del producto debe tener entre 5 y 132 caracteres.")]
		public string NombreProducto { get; set; }

		[Required(ErrorMessage = "La descripción del producto es requerida.")]
		[StringLength(255, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 255 caracteres.")]
		public string Descripcion { get; set; }

		[Required(ErrorMessage = "La URL de la imagen es requerida.")]
		[StringLength(1024, MinimumLength = 5, ErrorMessage = "La URL de la imagen debe tener entre 5 y 1024 caracteres.")]
		public string UrlImagen { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
