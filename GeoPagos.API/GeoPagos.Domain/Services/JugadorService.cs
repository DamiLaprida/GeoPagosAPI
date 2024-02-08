using GeoPagos.Domain.Contracts.Repository;
using GeoPagos.Domain.Contracts.Services;
using GeoPagos.Domain.Dtos;

namespace GeoPagos.Domain.Services
{
    public class JugadorService : IJugadorService
    {
        private readonly IJugadorRepository _jugadorRepository;
        
        public JugadorService(IJugadorRepository jugadorRepository)
        {
            _jugadorRepository = jugadorRepository;
        }

        public async Task<IEnumerable<JugadorDTO>> GetJugadoresByGeneroAsync(string genero)
        {
            if (String.IsNullOrEmpty(genero))
                throw new NullReferenceException("El campo 'Género' no debe ser nulo o estar vacío");

            var jugadores = await _jugadorRepository.GetJugadoresByGeneroAsync(genero);

            return jugadores;
        }
    }
}
