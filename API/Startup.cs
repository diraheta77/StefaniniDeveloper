using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using API.Persistencia;
using API.Aplicacion;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<RegistrarCliente>());

            //conexion a la base de datos...
            services.AddDbContext<ContextCientes>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("connBD"));
            });

            //para permitir definiciones de datos iguales en distintos metodos cuando se exponen en swagger
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
            });

            //add mediatR use
            services.AddMediatR(typeof(RegistrarCliente.InsertCliente).Assembly);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
