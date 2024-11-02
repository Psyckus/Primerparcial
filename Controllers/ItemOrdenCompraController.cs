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
	public class ItemOrdenCompraController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ItemOrdenCompraController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de ítems de una orden de compra por cantidad o precio
		[HttpGet("buscarItemOrden")]
		public async Task<IActionResult> buscarItemOrden(int? cantidad, decimal? precio)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var query = _context.ItemsOrdenCompra.AsQueryable();

			if (cantidad != null)
			{
				query = query.Where(ioc => ioc.Cantidad == cantidad);
			}

			if (precio != null)
			{
				query = query.Where(ioc => ioc.Precio == precio);
			}

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}
	}
}
