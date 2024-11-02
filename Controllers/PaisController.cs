using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;

namespace Parcial_1.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PaisController : Controller
	{
	
		private readonly AppDbContext _context;

		public PaisController(AppDbContext context)
		{
			_context = context;
		}

		// Método para buscar países por nombre
		[HttpGet("BuscarPaises")]
		public async Task<ActionResult<IEnumerable<Pais>>> BuscarPaises([FromQuery] Pais request)
		{
			// Validar el modelo
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var query = _context.Paises.AsQueryable();

			if (!string.IsNullOrWhiteSpace(request.Nombre))
			{
				query = query.Where(p => p.Nombre.Contains(request.Nombre));
			}

			var paises = await query.ToListAsync();

			if (!paises.Any())
			{
				return NotFound("No se encontraron países.");
			}

			return Ok(paises);
		}
	}
}
