#nullable disable

namespace GeoPagos.Domain.Entities
{
    public class Jugador
    {
        public int Id { get; set; }
        public int IdTorneo { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public int Habilidad { get; set; }
        public int Fuerza { get; set; }
        public int Velocidad { get; set; }
        public int TiempoReaccion { get; set; }

        public virtual Torneo TorneoNavigation { get; set; }
    }
}
