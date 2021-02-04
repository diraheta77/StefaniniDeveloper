using FrontEndLibro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndLibro.Interfaces
{
    interface IWriteable
    {
        void Update(Cliente cliente);
        void Create(Cliente cliente);

        void CreatePedido(Pedido pedido);
    }
}
