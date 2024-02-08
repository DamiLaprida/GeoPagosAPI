using AutoMapper;
using GeoPagos.Domain.Contracts.Repository;
using GeoPagos.Domain.Contracts.Services;
using GeoPagos.Domain.Services;
using GeoPagos.Persistence.Context;
using GeoPagos.Persistence.MappingProfile;
using GeoPagos.Persistence.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;

namespace GeoPagos.API
{
    public class Startup
    {
        private readonly ILogger _logger;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            _logger = loggerFactory.CreateLogger<Startup>();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var torneoDbConnectionString = Configuration.GetConnectionString("TorneoDb");

            services.AddDbContext<TorneoContext>(options =>
                options.UseSqlite(torneoDbConnectionString));
            services.AddTransient<IJugadorRepository, JugadorRepository>();
            services.AddTransient<IJugadorService, JugadorService>();
            services.AddTransient<ITorneoRepository, TorneoRepository>();
            services.AddTransient<ITorneoService, TorneoService>();

            services.AddHealthChecks();

            //Configuración automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiGeoPago", Version = "v1" }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "Local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiGeoPagos v1"));
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        _logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync("Internal Server Error.");
                    }
                });
                ;
            });

            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/api/healthcheck");
            });
        }
    }
}
