using GeoPagos.Domain.Dtos;

namespace GeoPagos.Domain.Contracts.Services
{
    public interface IJugadorService
    {
        Task<IEnumerable<JugadorDTO>> GetJugadoresByGeneroAsync(string genero);
    }
}
