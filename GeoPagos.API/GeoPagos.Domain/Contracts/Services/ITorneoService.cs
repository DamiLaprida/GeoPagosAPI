using GeoPagos.Domain.Dtos;

namespace GeoPagos.Domain.Contracts.Services
{
    public interface ITorneoService
    {
        Task<JugadorDTO> GetGanadorTorneoAsync(string genero);
        Task<IEnumerable<TorneoDTO>> GetResultadosTorneosAsync();
    }
}
