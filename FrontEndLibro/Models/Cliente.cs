using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndLibro.Interfaces;

namespace FrontEndLibro.Models
{
    public class Cliente
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
    }
}
