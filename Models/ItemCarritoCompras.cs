using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class ItemCarritoCompras
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int CarritoComprasId { get; set; }  // Relación con CarritoCompras

		[Required]
		public int ProductoId { get; set; }  // Relación con Producto

		[Required(ErrorMessage = "La cantidad es requerida.")]
		[Range(1, 9999, ErrorMessage = "La cantidad debe estar entre 1 y 9,999.")]
		public int Cantidad { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
