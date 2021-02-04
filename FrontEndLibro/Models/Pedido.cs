using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndLibro.Models
{
    public class Pedido
    {
        public int IDLibro { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string FechaPedido { get; set; }
    }
}
