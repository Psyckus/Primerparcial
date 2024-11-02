using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class CategoriaProducto
	{
		[Key]
		public int Id { get; set; }

		public int? CategoriaPadreId { get; set; }  // Relación opcional con la categoría padre

		[Required(ErrorMessage = "El nombre de la categoría es requerido.")]
		[StringLength(132, MinimumLength = 5, ErrorMessage = "El nombre de la categoría debe tener entre 5 y 132 caracteres.")]
		public string NombreCategoria { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
