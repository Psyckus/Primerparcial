using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class ResenaCliente
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int ClienteId { get; set; }  // Relación con Cliente

		[Required]
		public int ProductoId { get; set; }  // Relación con Producto

		[Required(ErrorMessage = "El valor de clasificación es requerido.")]
		[Range(0, 5, ErrorMessage = "El valor de clasificación debe estar entre 0 y 5.")]
		public int ValorClasificacion { get; set; }  // Rating

		[Required(ErrorMessage = "El comentario es requerido.")]
		[StringLength(255, MinimumLength = 5, ErrorMessage = "El comentario debe tener entre 5 y 255 caracteres.")]
		public string Comentario { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;

		// Propiedades de navegación
		public Cliente Cliente { get; set; }
		public Producto Producto { get; set; }

	}
}
