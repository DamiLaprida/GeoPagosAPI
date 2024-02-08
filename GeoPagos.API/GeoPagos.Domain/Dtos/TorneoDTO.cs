using GeoPagos.Domain.Entities;

#nullable disable
namespace GeoPagos.Domain.Dtos
{
    public class TorneoDTO
    {
        public string NombreTorneo { get; set; } 
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ganador { get; set; }
        public string Cancha { get; set; }
    }
}
