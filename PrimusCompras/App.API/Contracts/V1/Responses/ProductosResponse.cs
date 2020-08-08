using App.ContextoPrimusDb.Entidades;
using System;
using System.Collections.Generic;

namespace App.API.Contracts.V1.Responses
{
    public class ProductosResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double PrecioProducto { get; set; }
        public Categorias Categoria { get; set; }
        public IEnumerable<Marcas> Marca { get; set; }
    }
}
