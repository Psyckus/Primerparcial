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
	public class CaracteristicaProductoController : ControllerBase
	{
		private readonly AppDbContext _context;

		public CaracteristicaProductoController(AppDbContext context)
		{
			_context = context;
		}

		// Búsqueda de características de productos por nombre
		[HttpGet("BuscarCaracteristica")]
		public async Task<IActionResult> BuscarCaracteristica(string? nombre)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var resultados = await _context.CaracteristicasProducto
				.Where(cp => EF.Functions.Like(cp.Nombre, $"%{nombre}%"))
				.ToListAsync();

			return Ok(resultados);
		}
	}
}
