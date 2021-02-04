using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrontEndLibro.Models;
using FrontEndLibro.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FrontEndLibro.Controllers
{
    public class HomeController : Controller, IWriteable, IReadable
    {
        private IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Registrar(Cliente cliente)
        {
            Create(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult Pedido(int id)
        {
            Pedido pedido = new Pedido();
            var response = GetLibroData(id.ToString());
            var r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Pedido>>(response);
            pedido = r.FirstOrDefault();
            pedido.IDLibro = id;
            return View(pedido);
        }


        [HttpPost]
        public IActionResult Pedido(Pedido pedido)
        {
            CreatePedido(pedido);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void Update(Cliente client)
        {
            throw new NotImplementedException();
        }

        public void Create(Cliente client)
        {
            InvokeService.Post(_configuration["APIRouteCliente"], client, "");
        }

        public void CreatePedido(Pedido pedido)
        {
            InvokeService.Post(_configuration["APIRoutePedido"], pedido, "");
        }

        public string GetLibroData(string id)
        {
            return InvokeService.Get(_configuration["APILibro"], id).Message;
        }
    }
}
