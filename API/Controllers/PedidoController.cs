using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Aplicacion;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> InsertCliente(RegistrarPedido.InsertPedido pedido)
        {
            return await Mediator.Send(pedido);
        }
    }
}
