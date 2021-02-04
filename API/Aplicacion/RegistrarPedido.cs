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
    public class RegistrarPedido
    {
        public class InsertPedido : IRequest
        {
            public int IDLibro { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Publisher { get; set; }
            public string Language { get; set; }
            public string FechaPedido { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<InsertPedido>
        {
            public EjecutaValidacion()
            {
                RuleFor(d => d.IDLibro).NotEmpty();
                RuleFor(d => d.Title).NotEmpty();
                RuleFor(d => d.Language).NotEmpty();
                RuleFor(d => d.FechaPedido).NotEmpty();
            }
        }


        public class Manejador : IRequestHandler<InsertPedido>
        {
            private readonly ContextCientes _context;
            public Manejador(ContextCientes context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(InsertPedido request, CancellationToken cancellationToken)
            {
                try
                {
                    await AddPedido(request, cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new RestException(HttpStatusCode.InternalServerError, new { error = ex.Message });
                }
                return Unit.Value;
            }

            public async Task<int> AddPedido(InsertPedido request, CancellationToken cancellationToken)
            {
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@id",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.IDLibro
                        },
                        new SqlParameter() {
                            ParameterName = "@title",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.Title
                        },
                        new SqlParameter() {
                            ParameterName = "@author",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.Author
                        },
                        new SqlParameter() {
                            ParameterName = "@publisher",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.Publisher
                        },
                        new SqlParameter() {
                            ParameterName = "@languajebook",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.Language
                        },
                        new SqlParameter() {
                            ParameterName = "@FechaPedido",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = request.FechaPedido
                        }
                        };

                int affectedRows = await _context.Database.ExecuteSqlRawAsync("[dbo].[sp_insert_pedido] @id, @title, @author, @publisher, @languajebook, @FechaPedido", param);

                return affectedRows;
            }
        }


    }
}
