using GeoPagos.Domain.Entities;

namespace GeoPagos.Domain.Dtos
{
    public class RondaDTO
    {
        public int Numero { get; set; } //Número de ronda
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<Partido> Partidos { get; set; } = null!;
        public int TorneoId { get; set; }
    }
}
