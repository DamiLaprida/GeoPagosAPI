using AutoMapper;
using GeoPagos.Domain.Contracts.Repository;
using GeoPagos.Domain.Dtos;
using GeoPagos.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GeoPagos.Persistence.Repositories
{
    public class JugadorRepository : IJugadorRepository
    {
        private readonly TorneoContext _torneoContext;
        private readonly IMapper _mapper;
        
        public JugadorRepository(TorneoContext torneoContext, IMapper mapper)
        {
            _torneoContext = torneoContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JugadorDTO>> GetJugadoresByGeneroAsync(string genero)
        {
            var jugadores = _mapper.Map<IEnumerable<JugadorDTO>>(await _torneoContext.Jugadores.Where(j => j.Genero.ToLower() == genero.ToLower())
                .ToListAsync());

            return jugadores;
        }
    }
}
