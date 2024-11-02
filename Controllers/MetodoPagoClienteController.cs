using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;

namespace Parcial_1.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class MetodoPagoClienteController : Controller
	{
		private readonly AppDbContext _context;

		public MetodoPagoClienteController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de métodos de pago por proveedor, cuenta o fecha de expiración
		[HttpGet("buscarMetodoPago")]
		public async Task<IActionResult> buscarMetodoPago(string? proveedor, string? cuenta, DateTime? fechaExpira)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var query = _context.MetodoPagoCliente.AsQueryable();

			if (!string.IsNullOrEmpty(proveedor))
				query = query.Where(mp => EF.Functions.Like(mp.NombreProveedor, $"%{proveedor}%"));

			if (!string.IsNullOrEmpty(cuenta))
				query = query.Where(mp => EF.Functions.Like(mp.Cuenta, $"%{cuenta}%"));

			if (fechaExpira.HasValue)
				query = query.Where(mp => mp.FechaExpira.Date == fechaExpira.Value.Date);

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}
	
	}
}
