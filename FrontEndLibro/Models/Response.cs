using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndLibro.Models
{
    public class Response
    {
        public Response() { }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ResultType { get; set; }
        public object Result { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
