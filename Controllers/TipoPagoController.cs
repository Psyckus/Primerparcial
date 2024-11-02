using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;

namespace Parcial_1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TipoPagoController : Controller
	{
	

		private readonly AppDbContext _context;

		public TipoPagoController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de tipos de pago por descripción
		[HttpGet("buscarTipoPago")]
		public async Task<IActionResult> buscarTipoPago(string? descripcion)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var query = _context.TiposPago.AsQueryable();

			if (!string.IsNullOrEmpty(descripcion))
				query = query.Where(tp => EF.Functions.Like(tp.Descripcion, $"%{descripcion}%"));

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}
	
	}
}
