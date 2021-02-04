using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APILibro.Aplicacion;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APILibro.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> InsertCliente(RegistrarCliente.InsertCliente cliente)
        {
            return await Mediator.Send(cliente);
        }

        [HttpGet]
        public ActionResult GetMsjs()
        {
            return Ok("lalalala");
        }
    }
}
