using GeoPagos.Domain.Dtos;

namespace GeoPagos.Domain.Contracts.Repository
{
    public interface IJugadorRepository
    {
        Task<IEnumerable<JugadorDTO>> GetJugadoresByGeneroAsync(string genero);
    }
}
