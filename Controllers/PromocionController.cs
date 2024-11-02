using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial_1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PromocionController : ControllerBase
	{
		private readonly AppDbContext _context;

		public PromocionController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de promociones por descripción, porcentaje de descuento, fecha de inicio o finalización
		[HttpGet("buscarPromocion")]
		public async Task<IActionResult> buscarPromocion(string? descripcion, int? porcentajeDescuento, DateTime? fechaInicia, DateTime? fechaFinaliza)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var query = _context.Promociones.AsQueryable();

			if (!string.IsNullOrEmpty(descripcion))
				query = query.Where(p => EF.Functions.Like(p.Descripcion, $"%{descripcion}%"));

			if (porcentajeDescuento.HasValue)
				query = query.Where(p => p.PorcentajeDescuento == porcentajeDescuento.Value);

			if (fechaInicia.HasValue)
				query = query.Where(p => p.FechaInicia.Date == fechaInicia.Value.Date);

			if (fechaFinaliza.HasValue)
				query = query.Where(p => p.FechaFinaliza.Date == fechaFinaliza.Value.Date);

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}
	}
}
