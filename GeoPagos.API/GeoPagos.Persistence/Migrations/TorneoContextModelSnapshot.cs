﻿// <auto-generated />
using System;
using GeoPagos.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeoPagos.Persistence.Migrations
{
    [DbContext(typeof(TorneoContext))]
    partial class TorneoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.26");

            modelBuilder.Entity("GeoPagos.Domain.Entities.Jugador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Fuerza")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Genero")
                        .HasColumnType("TEXT");

                    b.Property<int>("Habilidad")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdTorneo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.Property<int>("TiempoReaccion")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Velocidad")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IdTorneo");

                    b.ToTable("Jugadores");
                });

            modelBuilder.Entity("GeoPagos.Domain.Entities.Ronda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("TEXT");

                    b.Property<int>("Numero")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TorneoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TorneoId");

                    b.ToTable("Rondas");
                });

            modelBuilder.Entity("GeoPagos.Domain.Entities.Torneo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cancha")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ganador")
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreTorneo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Torneos");
                });

            modelBuilder.Entity("GeoPagos.Domain.Entities.Jugador", b =>
                {
                    b.HasOne("GeoPagos.Domain.Entities.Torneo", "TorneoNavigation")
                        .WithMany("Jugadores")
                        .HasForeignKey("IdTorneo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TorneoNavigation");
                });

            modelBuilder.Entity("GeoPagos.Domain.Entities.Ronda", b =>
                {
                    b.HasOne("GeoPagos.Domain.Entities.Torneo", "IdTorneoRondaNavigation")
                        .WithMany("Rondas")
                        .HasForeignKey("TorneoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdTorneoRondaNavigation");
                });

            modelBuilder.Entity("GeoPagos.Domain.Entities.Torneo", b =>
                {
                    b.Navigation("Jugadores");

                    b.Navigation("Rondas");
                });
#pragma warning restore 612, 618
        }
    }
}
