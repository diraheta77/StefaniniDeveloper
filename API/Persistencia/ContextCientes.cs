using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Persistencia
{
    public class ContextCientes : DbContext
    {
        public ContextCientes(DbContextOptions<ContextCientes> options) : base(options)
        {
        }
    }
}
