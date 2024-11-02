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
	public class OrdenCompraController : ControllerBase
	{
		private readonly AppDbContext _context;

		public OrdenCompraController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de órdenes de compra por monto o estado
		[HttpGet("buscarOrdenCompra")]
		public async Task<IActionResult> buscarOrdenCompra(decimal? montoOrden, int? idEstadoOrden)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var query = _context.OrdenesCompra.AsQueryable();

			if (montoOrden != null)
			{
				query = query.Where(oc => oc.MontoOrden == montoOrden);
			}

			if (idEstadoOrden != null)
			{
				query = query.Where(oc => oc.IdEstadoOrden == idEstadoOrden);
			}

			var resultados = await query.ToListAsync();

			return Ok(resultados);
		}
	}
}
