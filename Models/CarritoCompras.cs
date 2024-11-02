using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class CarritoCompras
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int ClienteId { get; set; }  // Relación con Cliente

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
