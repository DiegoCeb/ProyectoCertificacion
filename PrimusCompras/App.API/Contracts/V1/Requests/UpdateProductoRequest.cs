using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Contracts.V1.Requests
{
    public class UpdateProductoRequest
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioProducto { get; set; }
    }
}
