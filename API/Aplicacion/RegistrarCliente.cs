using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using API.Persistencia;
using API.Modelo;
using System.Net;
using Microsoft.Data.SqlClient;

namespace API.Aplicacion
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
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Nombres",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.Nombres
                        },
                        new SqlParameter() {
                            ParameterName = "@Apellidos",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.Apellidos
                        },
                        new SqlParameter() {
                            ParameterName = "@FechaNacimiento",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.FechaNacimiento
                        },
                        new SqlParameter() {
                            ParameterName = "@Direccion",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.Direccion
                        },
                        new SqlParameter() {
                            ParameterName = "@Telefono",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.Telefono
                        },
                        new SqlParameter() {
                            ParameterName = "@Email",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.Email
                        },
                        };

                int affectedRows = await _context.Database.ExecuteSqlRawAsync("[dbo].[sp_insert_cliente] @Nombres, @Apellidos, @FechaNacimiento, @Direccion, @Telefono, @Email", param);
            
                return affectedRows;
            }


        }

    }
}
