using System.ComponentModel.DataAnnotations;

namespace Parcial_1.Models
{
	public class MetodoPagoCliente
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int ClienteId { get; set; }  // Relación con Cliente

		[Required]
		public int TipoPagoId { get; set; }  // Relación con TipoPago

		[Required(ErrorMessage = "El nombre del proveedor es requerido.")]
		[StringLength(255, MinimumLength = 5, ErrorMessage = "El nombre del proveedor debe tener entre 5 y 255 caracteres.")]
		public string NombreProveedor { get; set; }

		[Required(ErrorMessage = "La cuenta es requerida.")]
		[StringLength(24, ErrorMessage = "La cuenta debe tener máximo 24 caracteres (incluyendo guiones).")]
		public string Cuenta { get; set; }

		[Required(ErrorMessage = "La fecha de expiración es requerida.")]
		[DataType(DataType.Date)]
		[CustomValidation(typeof(MetodoPagoCliente), "ValidarFechaExpiracion")]
		public DateTime FechaExpira { get; set; }

		public bool PorDefecto { get; set; }

		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public DateTime FechaModificacion { get; set; } = DateTime.Now;

		// Método de validación personalizada
		public static ValidationResult ValidarFechaExpiracion(DateTime fechaExpira, ValidationContext context)
		{
			if (fechaExpira > DateTime.Now.AddYears(10))
			{
				return new ValidationResult("La fecha de expiración no debe ser mayor a 10 años desde la fecha actual.");
			}
			return ValidationResult.Success;
		}
	}
}
