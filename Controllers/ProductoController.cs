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
	public class ProductoController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ProductoController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de productos por nombre, descripción o URL de imagen
		[HttpGet("buscarProductos")]
		public async Task<IActionResult> buscarProductos(string? nombreProducto, string? descripcion, string? urlImagen)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var query = _context.Productos.AsQueryable();

			if (!string.IsNullOrEmpty(nombreProducto))
				query = query.Where(p => EF.Functions.Like(p.NombreProducto, $"%{nombreProducto}%"));

			if (!string.IsNullOrEmpty(descripcion))
				query = query.Where(p => EF.Functions.Like(p.Descripcion, $"%{descripcion}%"));

			if (!string.IsNullOrEmpty(urlImagen))
				query = query.Where(p => EF.Functions.Like(p.UrlImagen, $"%{urlImagen}%"));

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}
	}
}
