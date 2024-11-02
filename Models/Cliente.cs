using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class Cliente
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre y apellido es requerido.")]
		[StringLength(255, MinimumLength = 3, ErrorMessage = "El nombre y apellido debe tener entre 3 y 255 caracteres.")]
		public string NombreCompleto { get; set; }

		[Required(ErrorMessage = "El correo es requerido.")]
		[EmailAddress(ErrorMessage = "El correo no es válido.")]
		public string Correo { get; set; }

		[Required(ErrorMessage = "La clave es requerida.")]
		[MinLength(12, ErrorMessage = "La clave debe tener al menos 12 caracteres.")]
		public string Clave { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;
	}
}
