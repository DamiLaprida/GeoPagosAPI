using GeoPagos.Domain.Dtos;

namespace GeoPagos.Domain.Contracts.Repository
{
    public interface ITorneoRepository
    {
        Task<JugadorDTO> GetGanadorTorneoAsync(string genero);
        Task<IEnumerable<TorneoDTO>> GetResultadosTorneosAsync();
    }
}
