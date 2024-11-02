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
	public class OpcionCaracteristicaProductoController : ControllerBase
	{
		private readonly AppDbContext _context;

		public OpcionCaracteristicaProductoController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de opciones de una característica de producto por valor
		[HttpGet("buscarOpcionCaracteristica")]
		public async Task<IActionResult> buscarOpcionCaracteristica(string? valor)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var resultados = await _context.OpcionesCaracteristicaProducto
				.Where(ocp => EF.Functions.Like(ocp.Valor, $"%{valor}%"))
				.ToListAsync();

			return Ok(resultados);
		}
	}
}
