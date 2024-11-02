using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;

namespace Parcial_1.Controllers

{
	[ApiController]
	[Route("api/[controller]")]
	public class DireccionController : Controller
	{
		private readonly AppDbContext _context;

		public DireccionController(AppDbContext context)
		{
			_context = context;
		}
	
		// Método para buscar direcciones por dirección exacta o código postal
		[HttpGet("BuscarDirecciones")]
		public async Task<ActionResult<IEnumerable<Direccion>>> BuscarDirecciones([FromQuery] Direccion request)
		{
			// Validar el modelo
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var query = _context.Direcciones.AsQueryable();

			if (!string.IsNullOrWhiteSpace(request.DireccionExacta))
			{
				query = query.Where(d => d.DireccionExacta.Contains(request.DireccionExacta));
			}

			if (!string.IsNullOrWhiteSpace(request.CodigoPostal))
			{
				query = query.Where(d => d.CodigoPostal.Contains(request.CodigoPostal));
			}

			var direcciones = await query.ToListAsync();

			if (!direcciones.Any())
			{
				return NotFound("No se encontraron direcciones.");
			}

			return Ok(direcciones);
		}
	}
}
