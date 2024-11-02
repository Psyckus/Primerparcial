using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class DireccionCliente
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int ClienteId { get; set; }  // Relación con Cliente

		[Required]
		public int DireccionId { get; set; } // Relación con Dirección (si tienes otro modelo para dirección)

		public bool PorDefecto { get; set; } = false;  // Campo para indicar si es la dirección por defecto

		public DateTime FechaCreacion { get; set; } = DateTime.Now;

		public DateTime FechaModificacion { get; set; } = DateTime.Now;

		// Propiedad de navegación hacia el Cliente
		public Cliente Cliente { get; set; }
	}
}
