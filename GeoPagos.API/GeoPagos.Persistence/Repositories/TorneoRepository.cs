using AutoMapper;
using GeoPagos.Domain.Contracts.Repository;
using GeoPagos.Domain.Dtos;
using GeoPagos.Domain.Entities;
using GeoPagos.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GeoPagos.Persistence.Repositories
{
    public class TorneoRepository : ITorneoRepository
    {
        private readonly TorneoContext _torneoContext;
        private readonly IMapper _mapper;

        public TorneoRepository(TorneoContext torneoContext, IMapper mapper)
        {
            _torneoContext = torneoContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TorneoDTO>> GetResultadosTorneosAsync()
        {
            var torneosTerminados = _mapper.Map<IEnumerable<TorneoDTO>>
                (await _torneoContext.Torneos.ToListAsync());
            return torneosTerminados;
        }

        public async Task<JugadorDTO> GetGanadorTorneoAsync(string genero)
        {
            //Obtengo todos los jugadores 
            var jugadores = _torneoContext.Jugadores.Where(j => j.Genero.ToLower() == genero.ToLower())
                .ToList();

            //Creo el torneo 
            var torneo = new Torneo
            {
                NombreTorneo = $"Torneo {genero}",
                FechaInicio = new DateTime(2024, 02, 10),
                FechaFin = new DateTime(2024, 02, 20),
                Cancha = "Ladrillo",
                Jugadores = jugadores
            };

            _torneoContext.Torneos.Add(torneo);
             await _torneoContext.SaveChangesAsync();

            //Crea las rondas
            while (jugadores.Count > 1)
            {
                var ronda = new Ronda
                {
                    Numero = jugadores.Count / 2 + 1,
                    FechaInicio = torneo.FechaInicio,
                    TorneoId = torneo.Id,
                    Partidos = new List<Partido>()
                };

                // Realizar los partidos de la ronda
                for (int i = 0; i < jugadores.Count; i += 2)
                {
                    var partido = new Partido
                    {
                        Jugador1 = jugadores[i],
                        Jugador2 = jugadores[i + 1],
                    };

                    // Calcular el ganador del partido
                    partido.Ganador = (genero.ToLower() == "masculino") ? CalcularGanadorMasculino(partido) : CalcularGanadorFemenino(partido);

                    ronda.Partidos.Add(partido);
                }

                // Agregar la ronda al contexto y guardar los cambios
                _torneoContext.Rondas.Add(ronda);
                await _torneoContext.SaveChangesAsync();

                // Actualizar la lista de jugadores con los ganadores de la ronda
                jugadores = ronda.Partidos.Select(p => p.Ganador).ToList();
            }
            // Al final del torneo, el ganador es el último participante restante
            
            torneo.Ganador = jugadores.FirstOrDefault()?.Nombre;
            await _torneoContext.SaveChangesAsync();

            return _mapper.Map<JugadorDTO>(jugadores.FirstOrDefault());
        }

        private static Jugador CalcularGanadorMasculino(Partido partido)
        {
            var velocidadJugador1 = partido.Jugador1.Velocidad;
            var habilidadJugador1 = partido.Jugador1.Habilidad;
            var fuerzaJugador1 = partido.Jugador1.Fuerza;
            var velocidadJugador2 = partido.Jugador2.Velocidad;
            var habilidadJugador2 = partido.Jugador2.Habilidad;
            var fuerzaJugador2 = partido.Jugador2.Fuerza;
            var suerte = new Random().Next(5);
            Jugador ganador = new Jugador();

            if (velocidadJugador1 > velocidadJugador2 && fuerzaJugador1 > fuerzaJugador2)
                ganador = partido.Jugador1;

            if (habilidadJugador2 > habilidadJugador1 && velocidadJugador2 > velocidadJugador1)
                ganador = partido.Jugador2;

            if (fuerzaJugador1 > fuerzaJugador2)
                ganador = partido.Jugador1;

            if (fuerzaJugador2 > fuerzaJugador1)
                ganador = partido.Jugador2;

            if (suerte == 0)
                ganador = partido.Jugador1;

            if (suerte == 1)
                ganador = partido.Jugador2;

            return ganador;
        }

        private static Jugador CalcularGanadorFemenino(Partido partido)
        {
            var habilidadJugadora1 = partido.Jugador1.Habilidad;
            var habilidadJugadora2 = partido.Jugador2.Habilidad;
            var tiempoReaccionJugadora1 = partido.Jugador1.TiempoReaccion;
            var tiempoReaccionJugadora2 = partido.Jugador2.TiempoReaccion;
            var suerte = new Random().Next(5);
            Jugador ganador = new Jugador();

            if (habilidadJugadora1 > habilidadJugadora2 && tiempoReaccionJugadora1 > tiempoReaccionJugadora2)
                ganador = partido.Jugador1;

            if (habilidadJugadora2 > habilidadJugadora1 && tiempoReaccionJugadora2 > tiempoReaccionJugadora1)
                ganador = partido.Jugador2;

            if (tiempoReaccionJugadora1 > tiempoReaccionJugadora2)
                ganador = partido.Jugador1;

            if (tiempoReaccionJugadora2 > tiempoReaccionJugadora1)
                ganador = partido.Jugador2;

            if (suerte == 0)
                ganador = partido.Jugador1;

            if (suerte == 1)
                ganador = partido.Jugador2;

            return ganador;
        }
    }
}
