using GeoPagos.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoPagos.API.Controllers
{
    [Route("api/torneos")]
    [ApiController]
    public class TorneosController : Controller
    {
        private readonly ITorneoService _torneoService;

        public TorneosController(ITorneoService torneoService)
        {
            _torneoService = torneoService;
        }

        [HttpGet]
        [Route("finalizados")]
        public async Task<IActionResult> GetResultadosTorneosAsync()
        {
            var torneosFinalizados = await _torneoService.GetResultadosTorneosAsync();
            return Ok(torneosFinalizados);
        }

        [HttpGet]
        [Route("ganador")]
        public async Task<IActionResult> GetGanadorTorneoAsync(string genero)
        {
            if (String.IsNullOrEmpty(genero))
                return BadRequest("El campo 'Genero' no puede ser nulo o estar vacío.");

            var ganador = await _torneoService.GetGanadorTorneoAsync(genero);
            return Ok(ganador);
        }
    }
}
