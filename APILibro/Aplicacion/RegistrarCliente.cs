using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using APILibro.Persistencia;
using APILibro.Modelo;
using System.Net;

namespace APILibro.Aplicacion
{
    public class RegistrarCliente
    {
        public class InsertCliente : IRequest
        {
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string FechaNacimiento { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<InsertCliente>
        {
            public EjecutaValidacion()
            {
                RuleFor(d => d.Nombres).NotEmpty();
                RuleFor(d => d.Apellidos).NotEmpty();
                RuleFor(d => d.Email).NotEmpty();
            }
        }


        public class Manejador : IRequestHandler<InsertCliente>
        {
            private readonly ContextCientes _context;
            public Manejador(ContextCientes context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(InsertCliente request, CancellationToken cancellationToken)
            {
                try
                {
                    await AddCliente(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new RestException(HttpStatusCode.InternalServerError, new { error = ex.Message });
                }
                return Unit.Value;
            }


            public async Task<int> AddCliente(InsertCliente request, CancellationToken cancellationToken)
            {
                var affected = 
                    await _context.Database.ExecuteSqlRawAsync("sp_insert_cliente {@Nombres}, {@Apellidos}, {@FechaNacimiento}, {@Direccion}, {@Telefono}, {@Email}", request.Nombres, request.Apellidos, request.FechaNacimiento, request.Direccion, request.Telefono, request.Email);
                return affected;
            }


        }

    }
}
