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
	public class ItemProductoController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ItemProductoController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de ítems de productos por código de barras, cantidad disponible, URL de imagen o precio
		[HttpGet("buscarItemProducto")]
		public async Task<IActionResult> buscarItemProducto(string? codigoBarras, int? cantidadDisponible, string? urlImagen, decimal? precio)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var query = _context.ItemsProducto.AsQueryable();

			if (!string.IsNullOrEmpty(codigoBarras))
				query = query.Where(ip => EF.Functions.Like(ip.CodigoBarras, $"%{codigoBarras}%"));

			if (cantidadDisponible.HasValue)
				query = query.Where(ip => ip.CantidadDisponible == cantidadDisponible.Value);

			if (!string.IsNullOrEmpty(urlImagen))
				query = query.Where(ip => EF.Functions.Like(ip.UrlImagen, $"%{urlImagen}%"));

			if (precio.HasValue)
				query = query.Where(ip => ip.Precio == precio.Value);

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}
	}
}
