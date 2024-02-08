using GeoPagos.Domain.Contracts.Repository;
using GeoPagos.Domain.Contracts.Services;
using GeoPagos.Domain.Dtos;

namespace GeoPagos.Domain.Services
{
    public class TorneoService : ITorneoService
    {
        private readonly ITorneoRepository _torneoRepository;

        public TorneoService(ITorneoRepository torneoRepository)
        {
            _torneoRepository = torneoRepository;
        }

        public async Task<IEnumerable<TorneoDTO>> GetResultadosTorneosAsync()
        {
            var torneosTerminados = await _torneoRepository.GetResultadosTorneosAsync();
            return torneosTerminados;
        }

        public async Task<JugadorDTO> GetGanadorTorneoAsync(string genero)
        {
            if (String.IsNullOrEmpty(genero))
                throw new NullReferenceException("El campo 'Genero' no debe ser nulo o estar vacío.");

            var ganador = await _torneoRepository.GetGanadorTorneoAsync(genero);
            return ganador;
        }
    }
}
