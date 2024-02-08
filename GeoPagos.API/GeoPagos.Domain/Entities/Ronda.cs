#nullable disable

namespace GeoPagos.Domain.Entities
{
    public class Ronda
    {
        public int Id { get; set; }
        public int Numero { get; set; } //Número de ronda
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<Partido> Partidos { get; set; } = null!;
        public int TorneoId { get; set; }

        public virtual Torneo IdTorneoRondaNavigation { get; set; }
    }
}
