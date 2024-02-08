using AutoMapper;
using GeoPagos.Domain.Dtos;
using GeoPagos.Domain.Entities;

namespace GeoPagos.Persistence.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Jugador, JugadorDTO>();
        }
    }
}
