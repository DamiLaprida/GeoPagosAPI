#nullable disable
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoPagos.Domain.Entities
{
    public class Torneo
    {
        public Torneo()
        {
            Jugadores = new HashSet<Jugador>();
            Rondas = new HashSet<Ronda>();
        }

        public int Id { get; set; }
        public string NombreTorneo { get; set; } 
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ganador { get; set; }
        public string Cancha { get; set; }

        public virtual ICollection<Jugador> Jugadores { get; set; }
        public virtual ICollection<Ronda> Rondas { get; set; }
    }
}
