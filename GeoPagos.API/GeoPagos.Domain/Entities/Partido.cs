#nullable disable
namespace GeoPagos.Domain.Entities
{
    public class Partido
    {
        public Jugador Jugador1 { get; set; }
        public Jugador Jugador2 { get; set; }
        public Jugador Ganador { get; set; }
    }
}
