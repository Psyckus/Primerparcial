using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parcial_1.Data;
using Parcial_1.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial_1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriaProductoController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CategoriaProductoController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de categorías de producto por nombre
		[HttpGet("buscarCategoria")]
		public async Task<IActionResult> buscarCategoria(string? nombreCategoria)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var query = _context.CategoriasProducto.AsQueryable();

			if (!string.IsNullOrEmpty(nombreCategoria))
				query = query.Where(cp => EF.Functions.Like(cp.NombreCategoria, $"%{nombreCategoria}%"));

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}
	}
}
